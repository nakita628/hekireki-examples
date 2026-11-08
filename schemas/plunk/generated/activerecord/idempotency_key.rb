class IdempotencyKey < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt

  validates :key, presence: true, uniqueness: { scope: :projectId }
  validates :method, presence: true
  validates :path, presence: true
  validates :expiresAt, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :idempotency_keys
end
