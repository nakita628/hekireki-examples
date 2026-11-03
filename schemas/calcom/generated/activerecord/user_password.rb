class UserPassword < ApplicationRecord
  self.table_name = "UserPassword"

  validates :hash, presence: true
  validates :userId, uniqueness: true

  belongs_to :user, foreign_key: "userId", inverse_of: :password
end
