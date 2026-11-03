class Revenue < ApplicationRecord
  self.table_name = "revenue"
  self.primary_key = "revenue_id"

  validates :event_id, presence: true
  validates :event_name, presence: true, length: { maximum: 50 }
  validates :currency, presence: true, length: { maximum: 10 }

  belongs_to :website, inverse_of: :revenue
  belongs_to :session, inverse_of: :revenue
end
