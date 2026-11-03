class Subscription < ApplicationRecord
  attribute :quantity, default: 1
  attribute :cancel_at_period_end, default: false

  enum :status, { incomplete: "incomplete", incomplete_expired: "incomplete_expired", active: "active", paused: "paused", trialing: "trialing", past_due: "past_due", canceled: "canceled", unpaid: "unpaid" },
       validate: true
  enum :interval, { month: "month", year: "year" }, validate: true

  validates :price_id, presence: true
  validates :subscription_item_id, presence: true
  validates :amount, presence: true
  validates :active, inclusion: { in: [ true, false ] }
  validates :currency, presence: true
  validates :period_start, presence: true
  validates :period_end, presence: true

  belongs_to :user, inverse_of: :subscriptions
  belongs_to :space, inverse_of: :subscriptions
end
