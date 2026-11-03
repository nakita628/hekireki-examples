class CloudSpendAlert < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :title, presence: true
  validates :threshold, presence: true

  belongs_to :org, class_name: "Organization", inverse_of: :cloud_spend_alerts
end
