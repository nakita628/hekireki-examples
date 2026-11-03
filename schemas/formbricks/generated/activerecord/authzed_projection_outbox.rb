class AuthzedProjectionOutbox < ApplicationRecord
  self.table_name = "AuthzedProjectionOutbox"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :isRevocation, default: false
  attribute :attempts, default: 0
  attribute :permanentFailures, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :targetType, presence: true
  validates :primaryId, presence: true
end
