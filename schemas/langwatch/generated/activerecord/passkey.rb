class Passkey < ApplicationRecord
  self.table_name = "Passkey"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :counter, default: 0
  attribute :backedUp, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :publicKey, presence: true
  validates :credentialID, presence: true
  validates :deviceType, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :passkeys
end
