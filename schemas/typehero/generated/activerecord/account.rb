class Account < ApplicationRecord
  self.table_name = "Account"
  self.inheritance_column = nil

  attribute :id, default: -> { Cuid.generate }

  validates :type, presence: true
  validates :provider, presence: true, uniqueness: { scope: :providerAccountId }
  validates :providerAccountId, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :accounts
end
