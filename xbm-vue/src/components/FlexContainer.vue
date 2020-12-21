<template>
<div class="flex-container">
<div class="flex-box flex-top" v-if="$slots.top" :style="topStyle">
<slot name="top"></slot>
</div>
<div class="vertical-bar" v-if="$slots.top">
<div class="inside-bar"></div>
</div>
<div class="flex-box flex-bottom clearfix">
<div class="flex-box flex-left" :style="leftStyle">
<slot name="left"></slot>
</div>
<div class="horizontal-bar">
  <div class="inside-bar"></div>
</div>
<div class="flex-box flex-right">
<slot name="right"></slot>
</div>
</div>
</div>
</template>
<script>
/*
调用示例：
<v-flex-container>
<div slot="top">
<h1>可选</h1>
</div>
<div slot="left">
<h1>左边</h1>
</div>
<div slot="right">
<h1>右边</h1>
</div>
</v-flex-container>
*/
// import $ from "webpack-zepto";
window.requestAnimationFrame = (function() {
  return (
    window.requestAnimationFrame ||
    window.webkitRequestAnimationFrame ||
    window.mozRequestAnimationFrame
  );
})();
export default {
  name: "flexContainer",
  props: [
    "leftWidth",
    "topHeight",
    "leftMinWidth",
    "leftMaxWidth",
    "topMinHeight"
  ],
  computed: {
    leftStyle: function() {
      let style = "";
      if (this.leftWidth) {
        style += `width:${this.leftWidth};`;
      }
      if (this.leftMinWidth) {
        style += `min-width:${this.leftMinWidth}`;
      }
      if (this.leftMaxWidth) {
        style += `max-width:${this.leftMaxWidth}`;
      }
      return style;
    },
    topStyle: function() {
      let style = "";
      if (this.topHeight) {
        style += `height:${this.topHeight}`;
      }
      if (this.topMinHeight) {
        style += `min-height:${this.topMinHeight}`;
      }
      return style;
    }
  },
  mounted() {
    let $container = $(this.$el);
    let $document = $(document);
    if (this.$slots.top) {
      let $top = $(this.$el).find(".flex-top");
      let $bottom = $(this.$el).find(".flex-bottom");
      let $verticalBar = $(this.$el).find(".vertical-bar");
      $verticalBar.on("mousedown", function(e) {
        toggleNoSelect();
        $document.on("mousemove", onMousemove);
        $document.on("mouseup", onMouseup);
      });
      function onMousemove(e) {
        if (window.requestAnimationFrame) {
          requestAnimationFrame(() => {
            let y = e.pageY - $verticalBar.offset().top;
            let upHeight = $top.height();
            $top.height(upHeight + y);
          });
          return;
        }
        let y = e.pageY - $verticalBar.offset().top;
        let upHeight = $top.height();
        $top.height(upHeight + y);
      }
      function onMouseup() {
        $document.off("mousemove", onMousemove);
        $document.off("mouseup", onMouseup);
        toggleNoSelect();
      }
      function toggleNoSelect() {
        $container.toggleClass("noselect");
      }
    }
    let $left = $(this.$el).find(".flex-left");
    let $right = $(this.$el).find(".flex-right");
    let $horizontalBar = $(this.$el).find(".horizontal-bar");
    $horizontalBar.on("mousedown", function(e) {
      toggleNoSelect();
      $document.on("mousemove", onMousemove);
      $document.on("mouseup", onMouseup);
    });
    function onMousemove(e) {
      if (window.requestAnimationFrame) {
        requestAnimationFrame(() => {
          let x = e.pageX - $horizontalBar.offset().left;
          let leftWidth = $left.width();
          $left.width(leftWidth + x);
        });
        return;
      }
      let x = e.pageX - $horizontalBar.offset().left;
      let leftWidth = $left.width();
      $left.width(leftWidth + x);
    }
    function onMouseup() {
      $document.off("mousemove", onMousemove);
      $document.off("mouseup", onMouseup);
      toggleNoSelect();
    }
    function toggleNoSelect() {
      $container.toggleClass("noselect");
    }
  }
};
</script>
<style lang="scss">
.flex-container {
  position: relative;
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  &.noselect {
    user-select: none;
  }
  .flex-box {
    position: relative;
    overflow: hidden;
  }
  .flex-top {
    width: 100%;
    height: 30%;
    flex-shrink: 0;
    max-height: 90%;
    min-height: 10%;
  }
  .vertical-bar {
    width: 100%;
    height: 5px;
    box-shadow: inset 0 0 3px 1px rgba(0, 0, 0, 0.4);
    opacity: 0.6;
    cursor: row-resize;
    .inside-bar {
      width: 30px;
      height: 100%;
      margin: auto;
      background-color: #20a0ff;
    }
  }
  .flex-bottom {
    flex: 1;
    width: 100%;
    min-height: 5px;
    display: flex;
    flex-direction: row;
    height: 100%;
    .horizontal-bar {
      width: 5px;
      height: 100%;
      box-shadow: inset 0 0 3px 1px rgba(0, 0, 0, 0.4);
      opacity: 0.6;
      cursor: col-resize;
      position: relative;
      .inside-bar {
        width: 100%;
        height: 30px;
        position: absolute;
        top: 0;
        bottom: 0;
        margin: auto;
        background-color: #20a0ff;
      }
    }
    .flex-left {
      width: 25%;
      height: 100%;
      flex-shrink: 0;
      max-width: 90%;
      min-width: 15%;
      overflow-x: hidden;
    }
    .flex-right {
      flex: 1;
      height: 100%;
      overflow-x: hidden;
    }
  }
}
</style>