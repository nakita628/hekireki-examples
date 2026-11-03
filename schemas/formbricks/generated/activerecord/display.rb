class Display < ApplicationRecord
  self.table_name = "Display"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  belongs_to :survey, foreign_key: "surveyId", inverse_of: :displays
  belongs_to :contact, foreign_key: "contactId", optional: true, inverse_of: :displays
  has_one :response, foreign_key: "displayId", inverse_of: :display, dependent: :nullify
end
