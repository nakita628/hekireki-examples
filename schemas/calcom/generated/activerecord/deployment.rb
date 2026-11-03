class Deployment < ApplicationRecord
  self.table_name = "Deployment"

  attribute :id, default: 1
end
