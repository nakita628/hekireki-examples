class AttachmentSource < ApplicationRecord
  self.table_name = "AttachmentSource"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { file: "FILE", folder: "FOLDER" }, validate: true

  validates :name, presence: true
  validates :sourceId, presence: true, uniqueness: { scope: [ :ruleId, :driveConnectionId, :type ] }

  belongs_to :rule, foreign_key: "ruleId", inverse_of: :attachment_sources
  belongs_to :drive_connection, foreign_key: "driveConnectionId", inverse_of: :attachment_sources
  has_many :documents, class_name: "AttachmentDocument", foreign_key: "attachmentSourceId",
                       inverse_of: :attachment_source, dependent: :destroy
end
