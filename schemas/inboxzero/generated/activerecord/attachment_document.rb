class AttachmentDocument < ApplicationRecord
  self.table_name = "AttachmentDocument"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :fileId, presence: true, uniqueness: { scope: :attachmentSourceId }
  validates :name, presence: true
  validates :mimeType, presence: true

  belongs_to :attachment_source, foreign_key: "attachmentSourceId", inverse_of: :documents
end
