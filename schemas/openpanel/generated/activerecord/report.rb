class Report < ApplicationRecord
  attribute :range, default: "30d"
  attribute :lineType, default: "monotone"
  attribute :globalFilters, default: -> { JSON.parse("[]") }
  attribute :previous, default: false
  attribute :visibleSeries, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :interval, { hour: "hour", day: "day", month: "month", minute: "minute", week: "week" }, validate: true
  enum :chartType, { linear: "linear", bar: "bar", histogram: "histogram", pie: "pie", metric: "metric", area: "area", map: "map", funnel: "funnel", retention: "retention", conversion: "conversion", sankey: "sankey" },
       prefix: true, validate: true
  enum :metric, { sum: "sum", average: "average", min: "min", max: "max", count: "count" }, default: :sum, prefix: true,
                                                                                            validate: true

  validates :name, presence: true
  validates :breakdowns, exclusion: { in: [ nil ] }
  validates :events, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :reports
  belongs_to :dashboard, foreign_key: "dashboardId", inverse_of: :reports
  has_one :layout, class_name: "ReportLayout", foreign_key: "reportId", inverse_of: :report, dependent: :destroy
  has_one :share, class_name: "ShareReport", foreign_key: "reportId", inverse_of: :report, dependent: :destroy
end
