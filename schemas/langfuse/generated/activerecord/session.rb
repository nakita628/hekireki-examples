class Session < ApplicationRecord
  self.table_name = "Session"

  attribute :id, default: -> { Cuid.generate }

  validates :session_token, presence: true, uniqueness: true
  validates :expires, presence: true

  belongs_to :user, inverse_of: :sessions
end
