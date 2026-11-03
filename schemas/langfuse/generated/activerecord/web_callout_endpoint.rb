class WebCalloutEndpoint < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :name, default: "Default"
  attribute :enabled, default: true
  attribute :toast_message, default: "Callout sent"
  attribute :request_header_keys, default: -> { [] }

  validates :url, presence: true

  belongs_to :project, inverse_of: :web_callout_endpoints
end
