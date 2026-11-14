defmodule LangWatch.OrganizationSeatPeakMonth do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          organization_id: String.t(),
          month: String.t(),
          full_peak: integer(),
          full_peak_at: float(),
          lite_peak: integer(),
          lite_peak_at: float(),
          full_at_end: integer(),
          lite_at_end: integer()
        }

  schema "OrganizationSeatPeakMonth" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:month, :string, primary_key: true)
    field(:full_peak, :integer, source: :fullPeak)
    field(:full_peak_at, :float, source: :fullPeakAt)
    field(:lite_peak, :integer, source: :litePeak)
    field(:lite_peak_at, :float, source: :litePeakAt)
    field(:full_at_end, :integer, source: :fullAtEnd)
    field(:lite_at_end, :integer, source: :liteAtEnd)
  end
end