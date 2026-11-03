class PermissionGroupAccessControls < ApplicationRecord
  self.table_name = "PermissionGroupAccessControls"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :canView, default: true
  attribute :canDownload, default: false
  attribute :canDownloadOriginal, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :itemType, { dataroom_document: "DATAROOM_DOCUMENT", dataroom_folder: "DATAROOM_FOLDER" }, validate: true

  validates :itemId, presence: true, uniqueness: { scope: :groupId }

  belongs_to :group, class_name: "PermissionGroup", foreign_key: "groupId", inverse_of: :access_controls
end
