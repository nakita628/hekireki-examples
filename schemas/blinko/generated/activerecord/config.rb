class Config < ApplicationRecord
  self.table_name = "config"

  attribute :key, default: ""

  belongs_to :user, class_name: "Accounts", foreign_key: "userId", optional: true, inverse_of: :configs
end
