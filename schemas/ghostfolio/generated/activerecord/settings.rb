class Settings < ApplicationRecord
  self.table_name = "Settings"
  self.primary_key = "userId"

  alias_attribute :updated_at, :updatedAt

  belongs_to :user, foreign_key: "userId", inverse_of: :settings
end
