class AgreementResponse < ApplicationRecord
  self.table_name = "AgreementResponse"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :signingStatus, default: "PENDING"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :signedFileStorageType, { s3_path: "S3_PATH", vercel_blob: "VERCEL_BLOB" }, validate: { allow_nil: true }

  validates :viewId, uniqueness: { allow_nil: true }
  validates :signingExternalId, uniqueness: { allow_nil: true }

  belongs_to :agreement, foreign_key: "agreementId", inverse_of: :responses
  belongs_to :view, foreign_key: "viewId", optional: true, inverse_of: :agreement_response
end
