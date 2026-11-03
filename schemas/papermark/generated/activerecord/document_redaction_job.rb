class DocumentRedactionJob < ApplicationRecord
  self.table_name = "DocumentRedactionJob"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :status, default: "PENDING"
  attribute :customTerms, default: -> { [] }
  attribute :reasons, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :document, foreign_key: "documentId", inverse_of: :redaction_jobs
  belongs_to :document_version, foreign_key: "documentVersionId", inverse_of: :redaction_jobs
  belongs_to :team, foreign_key: "teamId", inverse_of: :redaction_jobs
  belongs_to :created_by, class_name: "User", foreign_key: "createdById", inverse_of: :created_redaction_jobs
  has_many :redactions, class_name: "DocumentRedaction", foreign_key: "jobId", inverse_of: :job, dependent: :destroy
end
