class DataroomBrand < ApplicationRecord
  self.table_name = "DataroomBrand"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :applyAccentColorToDataroomView, default: false
  attribute :cardLayout, default: "LIST"
  attribute :showFolderTree, default: true
  attribute :viewerLayoutPreset, default: "STANDARD"
  attribute :viewerHeaderStyle, default: "DEFAULT"
  attribute :hideFolderIconsInMain, default: false
  attribute :customLinkPreviewEnabled, default: false
  attribute :defaultLanguage, default: "en"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :dataroomId, uniqueness: true

  belongs_to :dataroom, foreign_key: "dataroomId", inverse_of: :brand
end
