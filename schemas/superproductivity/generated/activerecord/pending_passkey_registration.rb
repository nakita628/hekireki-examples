class PendingPasskeyRegistration < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :counter, default: 0

  validates :verification_token, presence: true, uniqueness: true
  validates :verification_token_expires_at, presence: true
  validates :credential_id, exclusion: { in: [ nil ] }
  validates :public_key, exclusion: { in: [ nil ] }

  belongs_to :user, inverse_of: :pending_passkey_registrations
end
