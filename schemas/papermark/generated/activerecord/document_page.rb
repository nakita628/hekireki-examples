class DocumentPage < ApplicationRecord
  self.table_name = "DocumentPage"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :embeddedLinks, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :storageType, { s3_path: "S3_PATH", vercel_blob: "VERCEL_BLOB" }, default: :vercel_blob, validate: true

  validates :pageNumber, presence: true, uniqueness: { scope: :versionId }
  validates :file, presence: true

  belongs_to :version, class_name: "DocumentVersion", foreign_key: "versionId", inverse_of: :pages
end
