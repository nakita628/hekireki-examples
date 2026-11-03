class MastraChannelConfig < ApplicationRecord
  self.table_name = "mastra_channel_config"
  self.primary_key = "platform"

  alias_attribute :updated_at, :updatedAt

  validates :data, exclusion: { in: [ nil ] }
end
