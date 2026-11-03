class Document < ApplicationRecord
  self.table_name = "Document"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :assistantEnabled, default: false
  attribute :advancedExcelEnabled, default: false
  attribute :agentsEnabled, default: false
  attribute :downloadOnly, default: false
  attribute :hiddenInAllDocuments, default: false
  attribute :isExternalUpload, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :storageType, { s3_path: "S3_PATH", vercel_blob: "VERCEL_BLOB" }, default: :vercel_blob, validate: true

  validates :name, presence: true
  validates :file, presence: true

  belongs_to :owner, class_name: "User", foreign_key: "ownerId", optional: true, inverse_of: :documents
  belongs_to :team, foreign_key: "teamId", inverse_of: :documents
  belongs_to :folder, foreign_key: "folderId", optional: true, inverse_of: :documents
  has_many :links, foreign_key: "documentId", inverse_of: :document, dependent: :destroy
  has_many :views, foreign_key: "documentId", inverse_of: :document, dependent: :destroy
  has_many :versions, class_name: "DocumentVersion", foreign_key: "documentId", inverse_of: :document,
                      dependent: :destroy
  has_many :chats, foreign_key: "documentId", inverse_of: :document, dependent: :destroy
  has_many :datarooms, class_name: "DataroomDocument", foreign_key: "documentId", inverse_of: :document,
                       dependent: :destroy
  has_many :tags, class_name: "TagItem", foreign_key: "documentId", inverse_of: :document, dependent: :destroy
  has_many :annotations, class_name: "DocumentAnnotation", foreign_key: "documentId", inverse_of: :document,
                         dependent: :destroy
  has_many :redaction_jobs, class_name: "DocumentRedactionJob", foreign_key: "documentId", inverse_of: :document,
                            dependent: :destroy
  has_many :uploaded_document, class_name: "DocumentUpload", foreign_key: "documentId", inverse_of: :document,
                               dependent: :destroy
end
