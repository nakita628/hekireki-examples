class Waitlist < ApplicationRecord
  self.table_name = "waitlist"

  validates :email, presence: true, uniqueness: true
end
