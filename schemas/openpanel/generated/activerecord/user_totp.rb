class UserTotp < ApplicationRecord
  self.table_name = "user_totp"

  attribute :recoveryCodes, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :userId, uniqueness: true
  validates :secret, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :totp
end
