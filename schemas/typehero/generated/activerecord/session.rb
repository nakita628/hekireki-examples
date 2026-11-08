class Session < ApplicationRecord
  self.table_name = "Session"

  attribute :id, default: -> { Cuid.generate }

  validates :sessionToken, presence: true, uniqueness: true
  validates :expires, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :sessions
end
