class HeatmapEvent < ApplicationRecord
  self.table_name = "heatmap_event"
  self.primary_key = "heatmap_event_id"

  validates :session_id, presence: true
  validates :visit_id, presence: true
  validates :url_path, presence: true, length: { maximum: 500 }
  validates :event_type, presence: true

  belongs_to :website, inverse_of: :heatmap_events
end
