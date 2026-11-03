class OAuthRecord < ApplicationRecord
  self.table_name = "OAuthRecord"

  alias_attribute :created_at, :createdAt

  validates :kind, presence: true
  validates :payload, exclusion: { in: [ nil ] }
end
