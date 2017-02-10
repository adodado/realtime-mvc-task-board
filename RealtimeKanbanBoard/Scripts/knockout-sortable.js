(function(factory) {
    if (typeof define === "function" && define.amd) {
        define(["knockout", "jquery", "jquery.ui.sortable"], factory);
    } else {
        factory(window.ko, jQuery);
    }
})(function(ko, $, undefined) {
    var ITEMKEY = "ko_sortItem",
        LISTKEY = "ko_sortList",
        PARENTKEY = "ko_parentList",
        DRAGKEY = "ko_dragItem",
        unwrap = ko.utils.unwrapObservable;

    var addMetaDataAfterRender = function(elements, data) {
        ko.utils.arrayForEach(elements, function(element) {
            if (element.nodeType === 1) {
                ko.utils.domData.set(element, ITEMKEY, data);
                ko.utils.domData.set(element, PARENTKEY, ko.utils.domData.get(element.parentNode, LISTKEY));
            }
        });
        $(".btnRemove").click(function() {
            var removeTaskId = $(this).data('id');
            var boardIds = $('#message').val();
            $.ajax({
                url: '/api/BoardTask/',
                type: "DELETE",
                data: JSON.stringify({ BoardId: boardIds, taskId: removeTaskId }), //ko.toJSON({BoardId:vm.boardId,name:listname}),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function() {
                    window.location.reload();
                }
            });
        });
        $(".btnRemoveColumn").click(function() {
            var taskname = $(this).next().text();
            var boardIds = $('#message').val();
            $.ajax({
                url: '/api/BoardListApi/',
                type: "DELETE",
                data: JSON.stringify({ BoardId: boardIds, name: taskname }), //ko.toJSON({BoardId:vm.boardId,name:listname}),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function() {
                    window.location.reload();
                }
            });
        });
    };

    var prepareTemplateOptions = function(valueAccessor, dataName) {
        var result = {},
            options = unwrap(valueAccessor()),
            actualAfterRender;

        if (options.data) {
            result[dataName] = options.data;
            result.name = options.template;
        } else {
            result[dataName] = valueAccessor();
        }

        ko.utils.arrayForEach(["afterAdd", "afterRender", "as", "beforeRemove", "includeDestroyed", "templateEngine", "templateOptions"], function(option) {
            result[option] = options[option] || ko.bindingHandlers.sortable[option];
        });

        if (dataName === "foreach") {
            if (result.afterRender) {
                actualAfterRender = result.afterRender;
                result.afterRender = function(element, data) {
                    addMetaDataAfterRender.call(data, element, data);
                    actualAfterRender.call(data, element, data);
                };
            } else {
                result.afterRender = addMetaDataAfterRender;
            }
        }

        return result;
    };

    ko.bindingHandlers.sortable = {
        init: function(element, valueAccessor, allBindingsAccessor, data, context) {
            var $element = $(element),
                value = unwrap(valueAccessor()) || {},
                templateOptions = prepareTemplateOptions(valueAccessor, "foreach"),
                sortable = {},
                startActual,
                updateActual;

            ko.utils.arrayForEach(element.childNodes, function(node) {
                if (node && node.nodeType === 3) {
                    node.parentNode.removeChild(node);
                }
            });

            $.extend(true, sortable, ko.bindingHandlers.sortable);
            if (value.options && sortable.options) {
                ko.utils.extend(sortable.options, value.options);
                delete value.options;
            }
            ko.utils.extend(sortable, value);

            if (sortable.connectClass && (ko.isObservable(sortable.allowDrop) || typeof sortable.allowDrop == "function")) {
                ko.computed({
                    read: function() {
                        var value = unwrap(sortable.allowDrop),
                            shouldAdd = typeof value == "function" ? value.call(this, templateOptions.foreach) : value;
                        ko.utils.toggleDomNodeCssClass(element, sortable.connectClass, shouldAdd);
                    },
                    disposeWhenNodeIsRemoved: element
                }, this);
            } else {
                ko.utils.toggleDomNodeCssClass(element, sortable.connectClass, sortable.allowDrop);
            }

            ko.bindingHandlers.template.init(element, function() { return templateOptions; }, allBindingsAccessor, data, context);

            startActual = sortable.options.start;
            updateActual = sortable.options.update;

            var createTimeout = setTimeout(function() {
                var dragItem;
                $element.sortable(ko.utils.extend(sortable.options, {
                    start: function(event, ui) {
                        ui.item.find("input:focus").change();
                        if (startActual) {
                            startActual.apply(this, arguments);
                        }
                    },
                    receive: function(event, ui) {
                        dragItem = ko.utils.domData.get(ui.item[0], DRAGKEY);
                        if (dragItem) {
                            if (dragItem.clone) {
                                dragItem = dragItem.clone();
                            }

                            if (sortable.dragged) {
                                dragItem = sortable.dragged.call(this, dragItem, event, ui) || dragItem;
                            }
                        }
                    },
                    update: function(event, ui) {
                        var sourceParent,
                            targetParent,
                            targetIndex,
                            i,
                            targetUnwrapped,
                            arg,
                            el = ui.item[0],
                            parentEl = ui.item.parent()[0],
                            item = ko.utils.domData.get(el, ITEMKEY) || dragItem;

                        dragItem = null;

                        if (item && (this === parentEl || $.contains(this, parentEl))) {
                            sourceParent = ko.utils.domData.get(el, PARENTKEY);
                            targetParent = ko.utils.domData.get(el.parentNode, LISTKEY);
                            targetIndex = ko.utils.arrayIndexOf(ui.item.parent().children(), el);

                            if (!templateOptions.includeDestroyed) {
                                targetUnwrapped = targetParent();
                                for (i = 0; i < targetIndex; i++) {
                                    if (targetUnwrapped[i] && unwrap(targetUnwrapped[i]._destroy)) {
                                        targetIndex++;
                                    }
                                }
                            }

                            if (sortable.beforeMove || sortable.afterMove) {
                                arg = {
                                    item: item,
                                    sourceParent: sourceParent,
                                    sourceParentNode: sourceParent && el.parentNode,
                                    sourceIndex: sourceParent && sourceParent.indexOf(item),
                                    targetParent: targetParent,
                                    targetIndex: targetIndex,
                                    cancelDrop: false
                                };
                            }

                            if (sortable.beforeMove) {
                                sortable.beforeMove.call(this, arg, event, ui);
                                if (arg.cancelDrop) {
                                    if (arg.sourceParent) {
                                        $(arg.sourceParent === arg.targetParent ? this : ui.sender).sortable('cancel');
                                    } else {
                                        $(el).remove();
                                    }

                                    return;
                                }
                            }

                            if (targetIndex >= 0) {
                                if (sourceParent) {
                                    sourceParent.remove(item);

                                    if (ko.processAllDeferredBindingUpdates) {
                                        ko.processAllDeferredBindingUpdates();
                                    }
                                }

                                targetParent.splice(targetIndex, 0, item);
                            }

                            ko.utils.domData.set(el, ITEMKEY, null);
                            ui.item.remove();

                            if (ko.processAllDeferredBindingUpdates) {
                                ko.processAllDeferredBindingUpdates();
                            }

                            if (sortable.afterMove) {
                                sortable.afterMove.call(this, arg, event, ui);
                            }
                        }

                        if (updateActual) {
                            updateActual.apply(this, arguments);
                        }
                    },
                    connectWith: sortable.connectClass ? "." + sortable.connectClass : false
                }));

                if (sortable.isEnabled !== undefined) {
                    ko.computed({
                        read: function() {
                            $element.sortable(unwrap(sortable.isEnabled) ? "enable" : "disable");
                        },
                        disposeWhenNodeIsRemoved: element
                    });
                }
            }, 0);

            ko.utils.domNodeDisposal.addDisposeCallback(element, function() {
                if ($element.data("sortable")) {
                    $element.sortable("destroy");
                }

                clearTimeout(createTimeout);
            });

            return { 'controlsDescendantBindings': true };
        },
        update: function(element, valueAccessor, allBindingsAccessor, data, context) {
            var templateOptions = prepareTemplateOptions(valueAccessor, "foreach");

            ko.utils.domData.set(element, LISTKEY, templateOptions.foreach);

            ko.bindingHandlers.template.update(element, function() { return templateOptions; }, allBindingsAccessor, data, context);
        },
        connectClass: 'ko_container',
        allowDrop: true,
        afterMove: null,
        beforeMove: null,
        options: {}
    };

    ko.bindingHandlers.draggable = {
        init: function(element, valueAccessor, allBindingsAccessor, data, context) {
            var value = unwrap(valueAccessor()) || {},
                options = value.options || {},
                draggableOptions = ko.utils.extend({}, ko.bindingHandlers.draggable.options),
                templateOptions = prepareTemplateOptions(valueAccessor, "data"),
                connectClass = value.connectClass || ko.bindingHandlers.draggable.connectClass,
                isEnabled = value.isEnabled !== undefined ? value.isEnabled : ko.bindingHandlers.draggable.isEnabled;

            value = value.data || value;

            ko.utils.domData.set(element, DRAGKEY, value);

            ko.utils.extend(draggableOptions, options);

            draggableOptions.connectToSortable = connectClass ? "." + connectClass : false;

            $(element).draggable(draggableOptions);

            if (isEnabled !== undefined) {
                ko.computed({
                    read: function() {
                        $(element).draggable(unwrap(isEnabled) ? "enable" : "disable");
                    },
                    disposeWhenNodeIsRemoved: element
                });
            }

            return ko.bindingHandlers.template.init(element, function() { return templateOptions; }, allBindingsAccessor, data, context);
        },
        update: function(element, valueAccessor, allBindingsAccessor, data, context) {
            var templateOptions = prepareTemplateOptions(valueAccessor, "data");

            return ko.bindingHandlers.template.update(element, function() { return templateOptions; }, allBindingsAccessor, data, context);
        },
        connectClass: ko.bindingHandlers.sortable.connectClass,
        options: {
            helper: "clone"
        }
    };
});