class Brand < ApplicationRecord
  self.table_name = "Brand"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :hideLogo, default: false
  attribute :applyAccentColorToDataroomView, default: false
  attribute :cardLayout, default: "LIST"
  attribute :showFolderTree, default: true
  attribute :viewerLayoutPreset, default: "STANDARD"
  attribute :viewerHeaderStyle, default: "DEFAULT"
  attribute :hideFolderIconsInMain, default: false
  attribute :customLinkPreviewEnabled, default: false
  attribute :defaultLanguage, default: "en"
  attribute :name, default: "Default"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, uniqueness: { scope: :teamId }

  belongs_to :team, foreign_key: "teamId", inverse_of: :brands
  has_many :default_for_team, class_name: "Team", foreign_key: "defaultBrandId", inverse_of: :default_brand,
                              dependent: :nullify
  has_many :links, foreign_key: "brandId", inverse_of: :brand, dependent: :nullify
  has_many :datarooms, foreign_key: "brandId", inverse_of: :base_brand, dependent: :nullify
end
