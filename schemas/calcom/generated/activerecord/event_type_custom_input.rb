class EventTypeCustomInput < ApplicationRecord
  self.table_name = "EventTypeCustomInput"
  self.inheritance_column = nil

  attribute :placeholder, default: ""

  enum :type, { text: "text", textlong: "textLong", number: "number", bool: "bool", radio: "radio", phone: "phone" },
       validate: true

  validates :label, presence: true
  validates :required, inclusion: { in: [ true, false ] }

  belongs_to :event_type, foreign_key: "eventTypeId", inverse_of: :custom_inputs
end
