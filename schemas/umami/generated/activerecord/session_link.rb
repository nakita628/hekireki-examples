class SessionLink < ApplicationRecord
  self.table_name = "session_link"
  self.primary_key = %w[website_id distinct_id session_id]

  validates :distinct_id, length: { maximum: 50 }
end
