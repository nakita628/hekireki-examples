class LiteLLMCacheConfig < ApplicationRecord
  self.table_name = "LiteLLM_CacheConfig"

  attribute :id, default: "cache_config"

  validates :cache_settings, exclusion: { in: [ nil ] }
end
