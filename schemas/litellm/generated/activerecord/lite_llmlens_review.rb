class LiteLLMLensReview < ApplicationRecord
  self.table_name = "LiteLLM_LensReview"
  self.primary_key = %w[lens_id criteria_key execution_id]

  validates :data, exclusion: { in: [ nil ] }

  belongs_to :lens, class_name: "LiteLLMLens", inverse_of: :reviews
end
