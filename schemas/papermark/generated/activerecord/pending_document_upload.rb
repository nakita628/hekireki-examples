class PendingDocumentUpload < ApplicationRecord
  self.table_name = "PendingDocumentUpload"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  enum :storageType, { s3_path: "S3_PATH", vercel_blob: "VERCEL_BLOB" }, default: :s3_path, validate: true

  validates :uploadId, presence: true, uniqueness: true
  validates :teamId, presence: true
  validates :tokenId, presence: true
  validates :key, presence: true
  validates :contentType, presence: true
  validates :expiresAt, presence: true
end
