defmodule LangWatch.SeatRequest do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          email: String.t(),
          user_id: String.t() | nil,
          name: String.t() | nil,
          status: String.t(),
          source: String.t(),
          idp_refused: boolean(),
          idp_kind: String.t() | nil,
          sales_notified_at: DateTime.t() | nil,
          opened_at: DateTime.t(),
          resolved_at: DateTime.t() | nil,
          resolved_by_user_id: String.t() | nil,
          seat_pending_since: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "SeatRequest" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:email, :string)
    field(:user_id, :string, source: :userId)
    field(:name, :string)
    field(:status, :string)
    field(:source, :string)
    field(:idp_refused, :boolean, default: false, source: :idpRefused)
    field(:idp_kind, :string, source: :idpKind)
    field(:sales_notified_at, LangWatch.PrismaDateTime, source: :salesNotifiedAt)
    field(:opened_at, LangWatch.PrismaDateTime, source: :openedAt)
    field(:resolved_at, LangWatch.PrismaDateTime, source: :resolvedAt)
    field(:resolved_by_user_id, :string, source: :resolvedByUserId)
    field(:seat_pending_since, LangWatch.PrismaDateTime, source: :seatPendingSince)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end