class Account < ApplicationRecord
  self.table_name = "Account"
  self.primary_key = %w[provider providerAccountId]
  self.inheritance_column = nil

  validates :type, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :accounts
end
