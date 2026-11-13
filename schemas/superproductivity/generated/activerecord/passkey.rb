class Passkey < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :counter, default: 0

  validates :credential_id, exclusion: { in: [ nil ] }, uniqueness: true
  validates :public_key, exclusion: { in: [ nil ] }

  belongs_to :user, inverse_of: :passkeys
end
