class DashboardWidget < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :min_version, default: 1

  enum :view, { traces: "TRACES", observations: "OBSERVATIONS", scores_numeric: "SCORES_NUMERIC", scores_categorical: "SCORES_CATEGORICAL", scores_boolean: "SCORES_BOOLEAN" },
       validate: true
  enum :chart_type, { line_time_series: "LINE_TIME_SERIES", area_time_series: "AREA_TIME_SERIES", bar_time_series: "BAR_TIME_SERIES", horizontal_bar: "HORIZONTAL_BAR", vertical_bar: "VERTICAL_BAR", pie: "PIE", number: "NUMBER", histogram: "HISTOGRAM", pivot_table: "PIVOT_TABLE" },
       validate: true

  validates :name, presence: true
  validates :description, presence: true
  validates :dimensions, exclusion: { in: [ nil ] }
  validates :metrics, exclusion: { in: [ nil ] }
  validates :filters, exclusion: { in: [ nil ] }
  validates :chart_config, exclusion: { in: [ nil ] }

  belongs_to :created_by_user, class_name: "User", foreign_key: "created_by", optional: true,
                               inverse_of: :dashboard_widgets_created
  belongs_to :updated_by_user, class_name: "User", foreign_key: "updated_by", optional: true,
                               inverse_of: :dashboard_widgets_updated
  belongs_to :project, optional: true, inverse_of: :dashboard_widget
end
