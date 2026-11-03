class DocumentUpload < ApplicationRecord
  self.table_name = "DocumentUpload"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :document, foreign_key: "documentId", inverse_of: :uploaded_document
  belongs_to :team, foreign_key: "teamId", inverse_of: :uploaded_documents
  belongs_to :viewer, foreign_key: "viewerId", optional: true, inverse_of: :uploaded_documents
  belongs_to :view, foreign_key: "viewId", optional: true, inverse_of: :uploaded_documents
  belongs_to :link, foreign_key: "linkId", inverse_of: :uploaded_documents
  belongs_to :dataroom, foreign_key: "dataroomId", optional: true, inverse_of: :uploaded_documents
  belongs_to :dataroom_document, foreign_key: "dataroomDocumentId", optional: true, inverse_of: :uploaded_documents
  belongs_to :task, foreign_key: "taskId", optional: true, inverse_of: :uploads
end
