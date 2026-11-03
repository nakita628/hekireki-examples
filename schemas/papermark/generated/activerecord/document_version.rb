class DocumentVersion < ApplicationRecord
  self.table_name = "DocumentVersion"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isPrimary, default: false
  attribute :isVertical, default: false
  attribute :hasPages, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :storageType, { s3_path: "S3_PATH", vercel_blob: "VERCEL_BLOB" }, default: :vercel_blob, validate: true

  validates :versionNumber, presence: true, uniqueness: { scope: :documentId }
  validates :file, presence: true

  belongs_to :document, foreign_key: "documentId", inverse_of: :versions
  has_many :pages, class_name: "DocumentPage", foreign_key: "versionId", inverse_of: :version, dependent: :destroy
  has_many :redaction_jobs, class_name: "DocumentRedactionJob", foreign_key: "documentVersionId",
                            inverse_of: :document_version, dependent: :destroy
end
