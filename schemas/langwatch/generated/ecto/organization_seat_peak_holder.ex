defmodule LangWatch.OrganizationSeatPeakHolder do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          organization_id: String.t(),
          holder_kind: String.t(),
          holder_id: String.t(),
          seat: String.t(),
          last_changed_at: float(),
          last_change_id: String.t() | nil,
          expires_at: float() | nil,
          expired_by_fold: boolean(),
          suppressed_by: String.t() | nil,
          restored_by_fold: boolean()
        }

  schema "OrganizationSeatPeakHolder" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:holder_kind, :string, primary_key: true, source: :holderKind)
    field(:holder_id, :string, primary_key: true, source: :holderId)
    field(:seat, :string)
    field(:last_changed_at, :float, source: :lastChangedAt)
    field(:last_change_id, :string, source: :lastChangeId)
    field(:expires_at, :float, source: :expiresAt)
    field(:expired_by_fold, :boolean, default: false, source: :expiredByFold)
    field(:suppressed_by, :string, source: :suppressedBy)
    field(:restored_by_fold, :boolean, default: false, source: :restoredByFold)
  end
end