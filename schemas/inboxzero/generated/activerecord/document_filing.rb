class DocumentFiling < ApplicationRecord
  self.table_name = "DocumentFiling"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :wasAsked, default: false
  attribute :wasCorrected, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { processing: "PROCESSING", pending: "PENDING", filed: "FILED", rejected: "REJECTED", error: "ERROR", preview: "PREVIEW" },
       default: :filed, validate: true

  validates :messageId, presence: true, uniqueness: { scope: [ :emailAccountId, :attachmentId ] }
  validates :attachmentId, presence: true
  validates :filename, presence: true
  validates :folderPath, presence: true
  validates :notificationMessageId, uniqueness: { allow_nil: true }

  belongs_to :drive_connection, foreign_key: "driveConnectionId", inverse_of: :document_filings
  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :document_filings
end
