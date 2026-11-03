class Feedback < ApplicationRecord
  self.table_name = "Feedback"

  validates :rating, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :feedback
end
