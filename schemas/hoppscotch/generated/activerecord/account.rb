class Account < ApplicationRecord
  self.table_name = "Account"

  attribute :id, default: -> { Cuid.generate }

  validates :provider, presence: true, uniqueness: { scope: :providerAccountId }
  validates :providerAccountId, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :provider_accounts
end
