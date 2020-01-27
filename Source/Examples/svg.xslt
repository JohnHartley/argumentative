<?xml version="1.0" encoding="UTF-8"?>
<!-- Exports an Argument to SVG 1.1 using File / Export / Transform -->
<!-- Tested with Inkscape 0.44 -->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="xml" indent="yes"/>
  <xsl:template match="/">
    <svg xmlns:svg="http://www.w3.org/2000/svg" viewBox="0 0 500px 500px" width="297mm" height="210mm">
      <xsl:apply-templates select="argument"/>
    </svg>
  </xsl:template>
  <xsl:template match="premise">
    <g id="premise">
      <rect x="{x}px" y="{y}px" width="{width}px" height="{height}px" fill="white" stroke="black"/>
      <xsl:call-template name="boxText"/>
    </g>
    <xsl:apply-templates select="reason|objection|helper"/>
  </xsl:template>
  <xsl:template match="reason|objection|helper">
    <line x1="{number(x)+number(width) div 2}px" y1="{number(y)}px" x2="{number(../x)+number(../width) div 2}px" y2="{number(../y)+number(../height)}px" stroke="black"/>
    <g id="{ref}">
      <rect id="rect{ref}" x="{x}px" y="{y}px" width="{width}px" height="{height}px" fill="white" stroke="black"/>
      <xsl:call-template name="boxText"/>
    </g>
    <xsl:apply-templates select="reason|objection|helper"/>
  </xsl:template>
  <xsl:template name="boxText">
    <flowRoot style="text-anchor:middle;text-align:center;padding:1px;font-size:{number(fontSize)+2}px">
      <flowRegion>
        <rect x="{x}px " y="{y}px" width="{width}px" height="{height}px"/>
      </flowRegion>
      <flowPara>
        <xsl:value-of select="title"/>
      </flowPara>
    </flowRoot>
  </xsl:template>
  <xsl:template match="text()"/>
</xsl:stylesheet>
