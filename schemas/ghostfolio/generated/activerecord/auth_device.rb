class AuthDevice < ApplicationRecord
  self.table_name = "AuthDevice"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :credentialId, exclusion: { in: [ nil ] }
  validates :credentialPublicKey, exclusion: { in: [ nil ] }
  validates :counter, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :auth_devices
end
