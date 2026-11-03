class LinkPreset < ApplicationRecord
  self.table_name = "LinkPreset"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :enableCustomMetaTag, default: false
  attribute :enableNotification, default: false
  attribute :emailProtected, default: true
  attribute :emailAuthenticated, default: false
  attribute :allowDownload, default: false
  attribute :enableAllowList, default: false
  attribute :allowList, default: -> { [] }
  attribute :enableDenyList, default: false
  attribute :denyList, default: -> { [] }
  attribute :enableScreenshotProtection, default: false
  attribute :enableConfidentialView, default: false
  attribute :enablePassword, default: false
  attribute :enableWatermark, default: false
  attribute :enableAgreement, default: false
  attribute :enableCustomFields, default: false
  attribute :showBanner, default: false
  attribute :isDefault, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :pId, uniqueness: { allow_nil: true }

  belongs_to :team, foreign_key: "teamId", inverse_of: :link_presets
end
