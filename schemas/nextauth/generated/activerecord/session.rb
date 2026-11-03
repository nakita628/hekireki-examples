class Session < ApplicationRecord
  self.table_name = "Session"

  attribute :expires, PrismaDateTime.new

  validates :sessionToken, presence: true, uniqueness: true
  validates :expires, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :sessions
end
