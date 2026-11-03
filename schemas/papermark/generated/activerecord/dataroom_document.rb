class DataroomDocument < ApplicationRecord
  self.table_name = "DataroomDocument"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :dataroomId, uniqueness: { scope: :documentId }

  belongs_to :dataroom, foreign_key: "dataroomId", inverse_of: :documents
  belongs_to :document, foreign_key: "documentId", inverse_of: :datarooms
  belongs_to :folder, class_name: "DataroomFolder", foreign_key: "folderId", optional: true, inverse_of: :documents
  has_many :conversations, foreign_key: "dataroomDocumentId", inverse_of: :dataroom_document, dependent: :nullify
  has_many :questions, class_name: "DataroomQuestion", foreign_key: "dataroomDocumentId", inverse_of: :dataroom_document,
                       dependent: :nullify
  has_many :dataroom_faqs, class_name: "DataroomFaqItem", foreign_key: "dataroomDocumentId",
                           inverse_of: :dataroom_document, dependent: :nullify
  has_many :uploaded_documents, class_name: "DocumentUpload", foreign_key: "dataroomDocumentId",
                                inverse_of: :dataroom_document, dependent: :nullify
end
