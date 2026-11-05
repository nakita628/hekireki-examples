class DigestRecord < ApplicationRecord
  self.table_name = "Digest"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", processing: "PROCESSING", sent: "SENT", failed: "FAILED" }, default: :pending,
                                                                                                  validate: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :digests
  has_many :items, class_name: "DigestItem", foreign_key: "digestId", inverse_of: :digest, dependent: :destroy
end
