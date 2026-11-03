class Passkey < ApplicationRecord
  self.table_name = "Passkey"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :transports, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :credentialId, exclusion: { in: [ nil ] }
  validates :credentialPublicKey, exclusion: { in: [ nil ] }
  validates :counter, presence: true
  validates :credentialDeviceType, presence: true
  validates :credentialBackedUp, inclusion: { in: [ true, false ] }

  belongs_to :user, foreign_key: "userId", inverse_of: :passkeys
end
