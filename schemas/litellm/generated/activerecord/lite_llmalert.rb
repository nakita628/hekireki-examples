class LiteLLMAlert < ApplicationRecord
  self.table_name = "LiteLLM_Alert"
  self.inheritance_column = nil

  attribute :status, default: "open"

  validates :detection_key, presence: true, length: { maximum: 64 }, uniqueness: true
  validates :type, presence: true, length: { maximum: 32 }
  validates :resource_type, presence: true, length: { maximum: 16 }
  validates :resource_id, presence: true, length: { maximum: 1024 }
  validates :status, length: { maximum: 16 }
  validates :window_start, presence: true
  validates :window_end, presence: true
  validates :evidence, exclusion: { in: [ nil ] }
  validates :updated_by, length: { maximum: 1024 }
end
