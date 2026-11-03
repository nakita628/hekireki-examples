class CscCredential < ApplicationRecord
  self.table_name = "CscCredential"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :providerId, presence: true
  validates :credentialId, presence: true
  validates :signatureAlgorithm, presence: true
  validates :keyType, presence: true
  validates :digestAlgorithm, presence: true
  validates :recipientId, uniqueness: true

  belongs_to :recipient, foreign_key: "recipientId", inverse_of: :csc_credential
end
