from sqlalchemy import ARRAY, BigInteger, Dialect, Enum, ForeignKey, Index, MetaData, String, Text, TypeDecorator, text
from sqlalchemy.dialects.postgresql import JSONB, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from datetime import datetime, timezone
import uuid as uuid_mod


class UtcDateTime(TypeDecorator[datetime]):
    impl = TIMESTAMP
    cache_ok = True

    def process_bind_param(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        if value is None or value.tzinfo is None:
            return value
        return value.astimezone(timezone.utc).replace(tzinfo=None)

    def process_result_value(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        return None if value is None else value.replace(tzinfo=timezone.utc)


class UtcDateTimeTz(TypeDecorator[datetime]):
    impl = TIMESTAMP
    cache_ok = True

    def __init__(self, precision: Optional[int] = None) -> None:
        super().__init__(timezone=True, precision=precision)

    def process_bind_param(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        return value if value is None or value.tzinfo else value.replace(tzinfo=timezone.utc)

    def process_result_value(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        return None if value is None else value.astimezone(timezone.utc)


def utc_now() -> datetime:
    return datetime.now(timezone.utc)


class Base(DeclarativeBase):
    metadata = MetaData(naming_convention={"pk": "%(table_name)s_pkey"})
    type_annotation_map = {datetime: UtcDateTime(precision=3), str: Text}


class LiteLLMBudgetTable(Base):
    __tablename__ = "LiteLLM_BudgetTable"

    budget_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[Optional[str]]
    max_budget: Mapped[Optional[float]]
    soft_budget: Mapped[Optional[float]]
    max_parallel_requests: Mapped[Optional[int]]
    tpm_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    rpm_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    tpd_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    model_max_budget: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    model_tpm_limit: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    model_rpm_limit: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    budget_duration: Mapped[Optional[str]]
    budget_limits: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    cap_policy: Mapped[Optional[str]]
    budget_reset_at: Mapped[Optional[datetime]]
    allowed_models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    temp_budget_increase: Mapped[Optional[float]]
    temp_budget_expiry: Mapped[Optional[datetime]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[str]

    customer_organization_defaults: Mapped[list["LiteLLMCustomerOrganizationTable"]] = relationship(foreign_keys="LiteLLMCustomerOrganizationTable.default_budget_template_id", back_populates="default_budget_template")
    customer_organizations: Mapped[list["LiteLLMCustomerOrganizationTable"]] = relationship(foreign_keys="LiteLLMCustomerOrganizationTable.budget_id", back_populates="budget")
    customer_teams: Mapped[list["LiteLLMCustomerTeamTable"]] = relationship(foreign_keys="LiteLLMCustomerTeamTable.budget_id", back_populates="budget")
    customer_members: Mapped[list["LiteLLMCustomerMemberTable"]] = relationship(foreign_keys="LiteLLMCustomerMemberTable.budget_id", passive_deletes="all", back_populates="budget")
    customer_member_overrides: Mapped[list["LiteLLMCustomerMemberTable"]] = relationship(foreign_keys="LiteLLMCustomerMemberTable.budget_override_id", passive_deletes="all", back_populates="budget_override")
    customer_team_overrides: Mapped[list["LiteLLMCustomerTeamTable"]] = relationship(foreign_keys="LiteLLMCustomerTeamTable.budget_override_id", passive_deletes="all", back_populates="budget_override")
    organization: Mapped[list["LiteLLMOrganizationTable"]] = relationship(back_populates="litellm_budget_table")
    projects: Mapped[list["LiteLLMProjectTable"]] = relationship(back_populates="litellm_budget_table")
    keys: Mapped[list["LiteLLMVerificationToken"]] = relationship(back_populates="litellm_budget_table")
    end_users: Mapped[list["LiteLLMEndUserTable"]] = relationship(back_populates="litellm_budget_table")
    tags: Mapped[list["LiteLLMTagTable"]] = relationship(back_populates="litellm_budget_table")
    model_access_groups: Mapped[list["LiteLLMModelAccessGroupBudgetTable"]] = relationship(back_populates="litellm_budget_table")
    team_membership: Mapped[list["LiteLLMTeamMembership"]] = relationship(back_populates="litellm_budget_table")
    organization_membership: Mapped[list["LiteLLMOrganizationMembership"]] = relationship(back_populates="litellm_budget_table")

class LiteLLMCustomerOrganizationTable(Base):
    __tablename__ = "LiteLLM_CustomerOrganizationTable"

    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    budget_template_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    budget_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_BudgetTable.budget_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_CustomerOrganizationTable_budget_id_fkey"))
    customer_org_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    alias: Mapped[Optional[str]]
    default_budget_template_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_BudgetTable.budget_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_CustomerOrganizationTable_default_budget_template__fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[str]

    __table_args__ = (
        Index("LiteLLM_CustomerOrganizationTable_spend_idx", "spend"),
    )

    budget: Mapped[Optional["LiteLLMBudgetTable"]] = relationship(foreign_keys=[budget_id], back_populates="customer_organizations")
    default_budget_template: Mapped[Optional["LiteLLMBudgetTable"]] = relationship(foreign_keys=[default_budget_template_id], back_populates="customer_organization_defaults")
    teams: Mapped[list["LiteLLMCustomerTeamTable"]] = relationship(passive_deletes="all", back_populates="parent_org")
    members: Mapped[list["LiteLLMCustomerMemberTable"]] = relationship(passive_deletes="all", back_populates="parent_org")

class LiteLLMCustomerTeamTable(Base):
    __tablename__ = "LiteLLM_CustomerTeamTable"

    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    customer_team_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    alias: Mapped[Optional[str]]
    parent_org_id: Mapped[str] = mapped_column(ForeignKey("LiteLLM_CustomerOrganizationTable.customer_org_id", ondelete="RESTRICT", onupdate="CASCADE", name="LiteLLM_CustomerTeamTable_parent_org_id_fkey"))
    budget_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_BudgetTable.budget_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_CustomerTeamTable_budget_id_fkey"))
    budget_override_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_BudgetTable.budget_id", ondelete="RESTRICT", onupdate="CASCADE", name="LiteLLM_CustomerTeamTable_budget_override_id_fkey"))
    budget_override_expires_at: Mapped[Optional[datetime]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[str]

    __table_args__ = (
        Index("LiteLLM_CustomerTeamTable_parent_org_id_idx", "parent_org_id"),
        Index("LiteLLM_CustomerTeamTable_spend_idx", "spend"),
    )

    parent_org: Mapped["LiteLLMCustomerOrganizationTable"] = relationship(back_populates="teams")
    budget: Mapped[Optional["LiteLLMBudgetTable"]] = relationship(foreign_keys=[budget_id], back_populates="customer_teams")
    budget_override: Mapped[Optional["LiteLLMBudgetTable"]] = relationship(foreign_keys=[budget_override_id], back_populates="customer_team_overrides")

class LiteLLMCustomerMemberTable(Base):
    __tablename__ = "LiteLLM_CustomerMemberTable"

    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    customer_member_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    alias: Mapped[Optional[str]]
    parent_org_id: Mapped[str] = mapped_column(ForeignKey("LiteLLM_CustomerOrganizationTable.customer_org_id", ondelete="RESTRICT", onupdate="CASCADE", name="LiteLLM_CustomerMemberTable_parent_org_id_fkey"))
    parent_team_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    budget_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_BudgetTable.budget_id", ondelete="RESTRICT", onupdate="CASCADE", name="LiteLLM_CustomerMemberTable_budget_id_fkey"))
    budget_override_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_BudgetTable.budget_id", ondelete="RESTRICT", onupdate="CASCADE", name="LiteLLM_CustomerMemberTable_budget_override_id_fkey"))
    budget_override_expires_at: Mapped[Optional[datetime]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[str]

    __table_args__ = (
        Index("LiteLLM_CustomerMemberTable_parent_org_id_idx", "parent_org_id"),
        Index("LiteLLM_CustomerMemberTable_spend_idx", "spend"),
    )

    parent_org: Mapped["LiteLLMCustomerOrganizationTable"] = relationship(back_populates="members")
    budget: Mapped[Optional["LiteLLMBudgetTable"]] = relationship(foreign_keys=[budget_id], back_populates="customer_members")
    budget_override: Mapped[Optional["LiteLLMBudgetTable"]] = relationship(foreign_keys=[budget_override_id], back_populates="customer_member_overrides")
    budget_increase_requests: Mapped[list["LiteLLMCustomerBudgetIncreaseRequest"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="customer_member")

class LiteLLMCustomerBudgetIncreaseRequest(Base):
    __tablename__ = "LiteLLM_CustomerBudgetIncreaseRequest"

    request_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    customer_member_id: Mapped[str] = mapped_column(ForeignKey("LiteLLM_CustomerMemberTable.customer_member_id", ondelete="CASCADE", onupdate="CASCADE", name="LiteLLM_CustomerBudgetIncreaseRequest_customer_member_id_fkey"))
    requested_max_budget: Mapped[float]
    expires_at: Mapped[Optional[datetime]]
    reason: Mapped[Optional[str]]
    status: Mapped[str] = mapped_column(default="pending_review", server_default=text("'pending_review'"))
    submitted_by: Mapped[str]
    submitted_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    reviewed_by: Mapped[Optional[str]]
    reviewed_at: Mapped[Optional[datetime]]
    review_notes: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_CustomerBudgetIncreaseRequest_status_idx", "status"),
        Index("LiteLLM_CustomerBudgetIncreaseRequest_customer_member_id_idx", "customer_member_id"),
    )

    customer_member: Mapped["LiteLLMCustomerMemberTable"] = relationship(back_populates="budget_increase_requests")

class LiteLLMCredentialsTable(Base):
    __tablename__ = "LiteLLM_CredentialsTable"

    credential_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    credential_name: Mapped[str]
    display_name: Mapped[Optional[str]]
    credential_values: Mapped[dict[str, Any]] = mapped_column(JSONB)
    credential_info: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[str]

    __table_args__ = (
        Index("LiteLLM_CredentialsTable_credential_name_key", "credential_name", unique=True),
    )

class LiteLLMProxyModelTable(Base):
    __tablename__ = "LiteLLM_ProxyModelTable"

    model_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    model_name: Mapped[str]
    litellm_params: Mapped[dict[str, Any]] = mapped_column(JSONB)
    model_info: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    blocked: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[str]

class LiteLLMAgentsTable(Base):
    __tablename__ = "LiteLLM_AgentsTable"

    agent_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    agent_name: Mapped[str]
    litellm_params: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    agent_card_params: Mapped[dict[str, Any]] = mapped_column(JSONB)
    static_headers: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    extra_headers: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    kill_switch: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    agent_access_groups: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    access_group_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    object_permission_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_ObjectPermissionTable.object_permission_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_AgentsTable_object_permission_id_fkey"))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    identity_managed: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    execution_mode: Mapped[str] = mapped_column(default="autonomous", server_default=text("'autonomous'"))
    tpm_limit: Mapped[Optional[int]]
    rpm_limit: Mapped[Optional[int]]
    session_tpm_limit: Mapped[Optional[int]]
    session_rpm_limit: Mapped[Optional[int]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[str]

    __table_args__ = (
        Index("LiteLLM_AgentsTable_agent_name_key", "agent_name", unique=True),
    )

    object_permission: Mapped[Optional["LiteLLMObjectPermissionTable"]] = relationship(back_populates="agents_table")
    retired_identities: Mapped[list["LiteLLMRetiredAgentIdentity"]] = relationship(back_populates="agent")
    identity: Mapped[Optional["LiteLLMAgentIdentity"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="agent")

class LiteLLMAgentIdentity(Base):
    __tablename__ = "LiteLLM_AgentIdentity"

    agent_id: Mapped[str] = mapped_column(ForeignKey("LiteLLM_AgentsTable.agent_id", ondelete="CASCADE", onupdate="CASCADE", name="LiteLLM_AgentIdentity_agent_id_fkey"), primary_key=True)
    active: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    provider: Mapped[str]
    issuer: Mapped[str]
    tenant_id: Mapped[str]
    client_id: Mapped[str]
    service_principal_id: Mapped[Optional[str]]
    required_roles: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    required_scopes: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: ["user_impersonation"], server_default=text("ARRAY['user_impersonation']"))
    revision: Mapped[str] = mapped_column(default=lambda: str(uuid_mod.uuid4()))
    last_authenticated_at: Mapped[Optional[datetime]]

    __table_args__ = (
        Index("LiteLLM_AgentIdentity_provider_tenant_id_client_id_key", "provider", "tenant_id", "client_id", unique=True),
        Index("LiteLLM_AgentIdentity_issuer_service_principal_id_key", "issuer", "service_principal_id", unique=True),
    )

    agent: Mapped["LiteLLMAgentsTable"] = relationship(back_populates="identity")

class LiteLLMRetiredAgentIdentity(Base):
    __tablename__ = "LiteLLM_RetiredAgentIdentity"

    binding_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    agent_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_AgentsTable.agent_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_RetiredAgentIdentity_agent_id_fkey"))
    provider: Mapped[str]
    issuer: Mapped[str]
    tenant_id: Mapped[str]
    client_id: Mapped[str]

    __table_args__ = (
        Index("LiteLLM_RetiredAgentIdentity_provider_tenant_id_client_id_key", "provider", "tenant_id", "client_id", unique=True),
    )

    agent: Mapped[Optional["LiteLLMAgentsTable"]] = relationship(back_populates="retired_identities")

class LiteLLMRetiredAgent(Base):
    __tablename__ = "LiteLLM_RetiredAgent"

    original_agent_id: Mapped[str] = mapped_column(primary_key=True)
    retired_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class LiteLLMVerifiedSubject(Base):
    __tablename__ = "LiteLLM_VerifiedSubject"

    subject_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    issuer: Mapped[str]
    tenant_id: Mapped[str]
    oid: Mapped[str]
    kind: Mapped[str] = mapped_column(default="human", server_default=text("'human'"))
    user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_UserTable.user_id", ondelete="CASCADE", onupdate="CASCADE", name="LiteLLM_VerifiedSubject_user_id_fkey"))
    verified_via: Mapped[str] = mapped_column(default="sso_interactive", server_default=text("'sso_interactive'"))
    verified_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("LiteLLM_VerifiedSubject_user_id_idx", "user_id"),
        Index("LiteLLM_VerifiedSubject_issuer_tenant_id_oid_key", "issuer", "tenant_id", "oid", unique=True),
    )

    user: Mapped[Optional["LiteLLMUserTable"]] = relationship(back_populates="verified_subjects")

class LiteLLMOrganizationTable(Base):
    __tablename__ = "LiteLLM_OrganizationTable"

    organization_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    organization_alias: Mapped[str]
    budget_id: Mapped[str] = mapped_column(ForeignKey("LiteLLM_BudgetTable.budget_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_OrganizationTable_budget_id_fkey"))
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSONB, default=lambda: {}, server_default=text("'{}'"))
    models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    model_spend: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    object_permission_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_ObjectPermissionTable.object_permission_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_OrganizationTable_object_permission_id_fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[str]

    litellm_budget_table: Mapped[Optional["LiteLLMBudgetTable"]] = relationship(back_populates="organization")
    object_permission: Mapped[Optional["LiteLLMObjectPermissionTable"]] = relationship(back_populates="organizations")
    teams: Mapped[list["LiteLLMTeamTable"]] = relationship(back_populates="litellm_organization_table")
    users: Mapped[list["LiteLLMUserTable"]] = relationship(back_populates="litellm_organization_table")
    keys: Mapped[list["LiteLLMVerificationToken"]] = relationship(back_populates="litellm_organization_table")
    members: Mapped[list["LiteLLMOrganizationMembership"]] = relationship(passive_deletes="all", back_populates="organization")

class LiteLLMModelTable(Base):
    __tablename__ = "LiteLLM_ModelTable"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    model_aliases: Mapped[Optional[dict[str, Any]]] = mapped_column("aliases", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[str]

    team: Mapped[Optional["LiteLLMTeamTable"]] = relationship(back_populates="litellm_model_table")

class LiteLLMTeamTable(Base):
    __tablename__ = "LiteLLM_TeamTable"

    team_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    team_alias: Mapped[Optional[str]]
    organization_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_OrganizationTable.organization_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_TeamTable_organization_id_fkey"))
    object_permission_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_ObjectPermissionTable.object_permission_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_TeamTable_object_permission_id_fkey"))
    admins: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    members: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    members_with_roles: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSONB, default=lambda: {}, server_default=text("'{}'"))
    max_budget: Mapped[Optional[float]]
    soft_budget: Mapped[Optional[float]]
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    max_parallel_requests: Mapped[Optional[int]]
    tpm_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    rpm_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    tpd_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    budget_duration: Mapped[Optional[str]]
    budget_reset_at: Mapped[Optional[datetime]]
    blocked: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    model_spend: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    model_max_budget: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    router_settings: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    team_member_permissions: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    access_group_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    policies: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    default_team_member_models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    budget_limits: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    model_id: Mapped[Optional[int]] = mapped_column(ForeignKey("LiteLLM_ModelTable.id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_TeamTable_model_id_fkey"))
    allow_team_guardrail_config: Mapped[bool] = mapped_column(default=False, server_default=text("false"))

    __table_args__ = (
        Index("LiteLLM_TeamTable_model_id_key", "model_id", unique=True),
        Index("LiteLLM_TeamTable_organization_id_idx", "organization_id"),
        Index("LiteLLM_TeamTable_team_alias_idx", "team_alias"),
        Index("LiteLLM_TeamTable_created_at_idx", "created_at"),
    )

    litellm_organization_table: Mapped[Optional["LiteLLMOrganizationTable"]] = relationship(back_populates="teams")
    litellm_model_table: Mapped[Optional["LiteLLMModelTable"]] = relationship(back_populates="team")
    object_permission: Mapped[Optional["LiteLLMObjectPermissionTable"]] = relationship(back_populates="teams")
    projects: Mapped[list["LiteLLMProjectTable"]] = relationship(back_populates="litellm_team_table")

class LiteLLMProjectTable(Base):
    __tablename__ = "LiteLLM_ProjectTable"

    project_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    project_alias: Mapped[Optional[str]]
    description: Mapped[Optional[str]]
    team_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_TeamTable.team_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_ProjectTable_team_id_fkey"))
    budget_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_BudgetTable.budget_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_ProjectTable_budget_id_fkey"))
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSONB, default=lambda: {}, server_default=text("'{}'"))
    models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    model_spend: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    model_rpm_limit: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    model_tpm_limit: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    blocked: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    object_permission_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_ObjectPermissionTable.object_permission_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_ProjectTable_object_permission_id_fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[str]

    litellm_team_table: Mapped[Optional["LiteLLMTeamTable"]] = relationship(back_populates="projects")
    litellm_budget_table: Mapped[Optional["LiteLLMBudgetTable"]] = relationship(back_populates="projects")
    object_permission: Mapped[Optional["LiteLLMObjectPermissionTable"]] = relationship(back_populates="projects")
    keys: Mapped[list["LiteLLMVerificationToken"]] = relationship(back_populates="litellm_project_table")

class LiteLLMDeletedTeamTable(Base):
    __tablename__ = "LiteLLM_DeletedTeamTable"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    team_id: Mapped[str]
    team_alias: Mapped[Optional[str]]
    organization_id: Mapped[Optional[str]]
    object_permission_id: Mapped[Optional[str]]
    admins: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    members: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    members_with_roles: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSONB, default=lambda: {}, server_default=text("'{}'"))
    max_budget: Mapped[Optional[float]]
    soft_budget: Mapped[Optional[float]]
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    max_parallel_requests: Mapped[Optional[int]]
    tpm_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    rpm_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    tpd_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    budget_duration: Mapped[Optional[str]]
    budget_reset_at: Mapped[Optional[datetime]]
    blocked: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    model_spend: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    model_max_budget: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    router_settings: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    team_member_permissions: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    access_group_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    policies: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    model_id: Mapped[Optional[int]]
    allow_team_guardrail_config: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[Optional[datetime]]
    updated_at: Mapped[Optional[datetime]]
    deleted_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    deleted_by: Mapped[Optional[str]]
    deleted_by_api_key: Mapped[Optional[str]]
    litellm_changed_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_DeletedTeamTable_team_id_idx", "team_id"),
        Index("LiteLLM_DeletedTeamTable_deleted_at_idx", "deleted_at"),
        Index("LiteLLM_DeletedTeamTable_organization_id_idx", "organization_id"),
        Index("LiteLLM_DeletedTeamTable_team_alias_idx", "team_alias"),
        Index("LiteLLM_DeletedTeamTable_created_at_idx", "created_at"),
    )

class LiteLLMUserTable(Base):
    __tablename__ = "LiteLLM_UserTable"

    user_id: Mapped[str] = mapped_column(primary_key=True)
    user_alias: Mapped[Optional[str]]
    team_id: Mapped[Optional[str]]
    sso_user_id: Mapped[Optional[str]]
    organization_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_OrganizationTable.organization_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_UserTable_organization_id_fkey"))
    object_permission_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_ObjectPermissionTable.object_permission_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_UserTable_object_permission_id_fkey"))
    password: Mapped[Optional[str]]
    password_reset_required: Mapped[Optional[bool]]
    last_breach_check_at: Mapped[Optional[datetime]]
    teams: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    user_role: Mapped[Optional[str]]
    max_budget: Mapped[Optional[float]]
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    user_email: Mapped[Optional[str]]
    models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSONB, default=lambda: {}, server_default=text("'{}'"))
    max_parallel_requests: Mapped[Optional[int]]
    tpm_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    rpm_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    budget_duration: Mapped[Optional[str]]
    budget_reset_at: Mapped[Optional[datetime]]
    allowed_cache_controls: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    policies: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    model_spend: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    model_max_budget: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    created_at: Mapped[Optional[datetime]] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_UserTable_sso_user_id_key", "sso_user_id", unique=True),
    )

    litellm_organization_table: Mapped[Optional["LiteLLMOrganizationTable"]] = relationship(back_populates="users")
    object_permission: Mapped[Optional["LiteLLMObjectPermissionTable"]] = relationship(back_populates="users")
    verified_subjects: Mapped[list["LiteLLMVerifiedSubject"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    organization_memberships: Mapped[list["LiteLLMOrganizationMembership"]] = relationship(passive_deletes="all", back_populates="user")
    invitations_created: Mapped[list["LiteLLMInvitationLink"]] = relationship(foreign_keys="LiteLLMInvitationLink.created_by", passive_deletes="all", back_populates="lite_llm_user_table_created")
    invitations_updated: Mapped[list["LiteLLMInvitationLink"]] = relationship(foreign_keys="LiteLLMInvitationLink.updated_by", passive_deletes="all", back_populates="lite_llm_user_table_updated")
    invitations_user: Mapped[list["LiteLLMInvitationLink"]] = relationship(foreign_keys="LiteLLMInvitationLink.user_id", passive_deletes="all", back_populates="lite_llm_user_table_user")

class LiteLLMObjectPermissionTable(Base):
    __tablename__ = "LiteLLM_ObjectPermissionTable"

    object_permission_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    mcp_servers: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    mcp_access_groups: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    mcp_tool_permissions: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    vector_stores: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    agents: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    agent_access_groups: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    blocked_tools: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    mcp_toolsets: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    search_tools: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    mcp_tool_search_enabled: Mapped[Optional[bool]]
    skills: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))

    teams: Mapped[list["LiteLLMTeamTable"]] = relationship(back_populates="object_permission")
    projects: Mapped[list["LiteLLMProjectTable"]] = relationship(back_populates="object_permission")
    verification_tokens: Mapped[list["LiteLLMVerificationToken"]] = relationship(back_populates="object_permission")
    organizations: Mapped[list["LiteLLMOrganizationTable"]] = relationship(back_populates="object_permission")
    users: Mapped[list["LiteLLMUserTable"]] = relationship(back_populates="object_permission")
    end_users: Mapped[list["LiteLLMEndUserTable"]] = relationship(back_populates="object_permission")
    agents_table: Mapped[list["LiteLLMAgentsTable"]] = relationship(back_populates="object_permission")

class LiteLLMMCPServerTable(Base):
    __tablename__ = "LiteLLM_MCPServerTable"

    server_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    server_name: Mapped[Optional[str]]
    alias: Mapped[Optional[str]]
    description: Mapped[Optional[str]]
    instructions: Mapped[Optional[str]]
    url: Mapped[Optional[str]]
    spec_path: Mapped[Optional[str]]
    transport: Mapped[str] = mapped_column(default="sse", server_default=text("'sse'"))
    auth_type: Mapped[Optional[str]]
    credentials: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    created_at: Mapped[Optional[datetime]] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    updated_at: Mapped[Optional[datetime]] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[Optional[str]]
    mcp_info: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    mcp_access_groups: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    allowed_tools: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    tool_name_to_display_name: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    tool_name_to_description: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    pinned_tools: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    extra_headers: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    static_headers: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    env_vars: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: [], server_default=text("'[]'"))
    status: Mapped[Optional[str]] = mapped_column(default="unknown", server_default=text("'unknown'"))
    last_health_check: Mapped[Optional[datetime]]
    health_check_error: Mapped[Optional[str]]
    command: Mapped[Optional[str]]
    args: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    env: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    issuer: Mapped[Optional[str]]
    authorization_url: Mapped[Optional[str]]
    token_url: Mapped[Optional[str]]
    registration_url: Mapped[Optional[str]]
    oauth2_flow: Mapped[Optional[str]]
    token_exchange_endpoint: Mapped[Optional[str]]
    audience: Mapped[Optional[str]]
    subject_token_type: Mapped[Optional[str]]
    token_exchange_profile: Mapped[Optional[str]]
    allow_all_keys: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    available_on_public_internet: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    delegate_auth_to_upstream: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    oauth_passthrough: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    dcr_bridge: Mapped[Optional[bool]]
    per_server_oauth_discovery: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    is_byok: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    byok_description: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    byok_api_key_help_url: Mapped[Optional[str]]
    source_url: Mapped[Optional[str]]
    timeout: Mapped[Optional[float]]
    max_concurrent_requests: Mapped[Optional[int]]
    rpm: Mapped[Optional[int]]
    approval_status: Mapped[Optional[str]] = mapped_column(default="active", server_default=text("'active'"))
    submitted_by: Mapped[Optional[str]]
    submitted_at: Mapped[Optional[datetime]]
    reviewed_at: Mapped[Optional[datetime]]
    review_notes: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_MCPServerTable_approval_status_idx", "approval_status"),
    )

class LiteLLMMCPToolsetTable(Base):
    __tablename__ = "LiteLLM_MCPToolsetTable"

    toolset_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    toolset_name: Mapped[str]
    description: Mapped[Optional[str]]
    tools: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: [], server_default=text("'[]'"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_MCPToolsetTable_toolset_name_key", "toolset_name", unique=True),
    )

class LiteLLMMCPUserCredentials(Base):
    __tablename__ = "LiteLLM_MCPUserCredentials"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    user_id: Mapped[str]
    server_id: Mapped[str]
    credential_b64: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_MCPUserCredentials_user_id_server_id_key", "user_id", "server_id", unique=True),
    )

class LiteLLMUserProviderCredentials(Base):
    __tablename__ = "LiteLLM_UserProviderCredentials"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    user_id: Mapped[str]
    credential_name: Mapped[str]
    provider: Mapped[str]
    credential_b64: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_UserProviderCredentials_credential_name_idx", "credential_name"),
        Index("LiteLLM_UserProviderCredentials_user_id_credential_name_key", "user_id", "credential_name", unique=True),
    )

class LiteLLMMCPUserEnvVars(Base):
    __tablename__ = "LiteLLM_MCPUserEnvVars"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    user_id: Mapped[str]
    server_id: Mapped[str]
    values_b64: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_MCPUserEnvVars_user_id_idx", "user_id"),
        Index("LiteLLM_MCPUserEnvVars_server_id_idx", "server_id"),
        Index("LiteLLM_MCPUserEnvVars_user_id_server_id_key", "user_id", "server_id", unique=True),
    )

class LiteLLMMCPServerOAuthClient(Base):
    __tablename__ = "LiteLLM_MCPServerOAuthClient"

    server_id: Mapped[str] = mapped_column(primary_key=True)
    credentials: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

class LiteLLMSSOIdentityAssertion(Base):
    __tablename__ = "LiteLLM_SSOIdentityAssertion"

    user_id: Mapped[str] = mapped_column(primary_key=True)
    assertion_b64: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

class LiteLLMVerificationToken(Base):
    __tablename__ = "LiteLLM_VerificationToken"

    token: Mapped[str] = mapped_column(primary_key=True)
    key_name: Mapped[Optional[str]]
    key_alias: Mapped[Optional[str]]
    soft_budget_cooldown: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    total_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    expires: Mapped[Optional[datetime]]
    models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    aliases: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    config: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    router_settings: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    user_id: Mapped[Optional[str]]
    team_id: Mapped[Optional[str]]
    agent_id: Mapped[Optional[str]]
    project_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_ProjectTable.project_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_VerificationToken_project_id_fkey"))
    permissions: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    max_parallel_requests: Mapped[Optional[int]]
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSONB, default=lambda: {}, server_default=text("'{}'"))
    blocked: Mapped[Optional[bool]]
    tpm_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    rpm_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    tpd_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    max_budget: Mapped[Optional[float]]
    budget_duration: Mapped[Optional[str]]
    budget_reset_at: Mapped[Optional[datetime]]
    allowed_cache_controls: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    allowed_routes: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    key_type: Mapped[Optional[str]]
    policies: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    access_group_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    model_spend: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    model_max_budget: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    budget_fallbacks: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    budget_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_BudgetTable.budget_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_VerificationToken_budget_id_fkey"))
    organization_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_OrganizationTable.organization_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_VerificationToken_organization_id_fkey"))
    object_permission_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_ObjectPermissionTable.object_permission_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_VerificationToken_object_permission_id_fkey"))
    created_at: Mapped[Optional[datetime]] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    updated_at: Mapped[Optional[datetime]] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[Optional[str]]
    settings_updated_at: Mapped[Optional[datetime]]
    last_active: Mapped[Optional[datetime]]
    rotation_count: Mapped[Optional[int]] = mapped_column(default=0, server_default=text("0"))
    auto_rotate: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    rotation_interval: Mapped[Optional[str]]
    last_rotation_at: Mapped[Optional[datetime]]
    key_rotation_at: Mapped[Optional[datetime]]
    budget_limits: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

    __table_args__ = (
        Index("LiteLLM_VerificationToken_user_id_team_id_idx", "user_id", "team_id"),
        Index("LiteLLM_VerificationToken_team_id_idx", "team_id"),
        Index("LiteLLM_VerificationToken_budget_reset_at_expires_idx", "budget_reset_at", "expires"),
    )

    litellm_budget_table: Mapped[Optional["LiteLLMBudgetTable"]] = relationship(back_populates="keys")
    litellm_organization_table: Mapped[Optional["LiteLLMOrganizationTable"]] = relationship(back_populates="keys")
    litellm_project_table: Mapped[Optional["LiteLLMProjectTable"]] = relationship(back_populates="keys")
    object_permission: Mapped[Optional["LiteLLMObjectPermissionTable"]] = relationship(back_populates="verification_tokens")
    jwt_key_mappings: Mapped[list["LiteLLMJWTKeyMapping"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="litellm_verification_token")

class LiteLLMJWTKeyMapping(Base):
    __tablename__ = "LiteLLM_JWTKeyMapping"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    jwt_issuer: Mapped[str] = mapped_column(default="", server_default=text("''"))
    jwt_claim_name: Mapped[str]
    jwt_claim_value: Mapped[str]
    token: Mapped[str] = mapped_column(ForeignKey("LiteLLM_VerificationToken.token", ondelete="CASCADE", onupdate="CASCADE", name="LiteLLM_JWTKeyMapping_token_fkey"))
    description: Mapped[Optional[str]]
    is_active: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_v_idx", "jwt_issuer", "jwt_claim_name", "jwt_claim_value", "is_active"),
        Index("LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_v_key", "jwt_issuer", "jwt_claim_name", "jwt_claim_value", unique=True),
    )

    litellm_verification_token: Mapped["LiteLLMVerificationToken"] = relationship(back_populates="jwt_key_mappings")

class LiteLLMDeprecatedVerificationToken(Base):
    __tablename__ = "LiteLLM_DeprecatedVerificationToken"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    token: Mapped[str]
    active_token_id: Mapped[str]
    revoke_at: Mapped[datetime]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("LiteLLM_DeprecatedVerificationToken_token_revoke_at_idx", "token", "revoke_at"),
        Index("LiteLLM_DeprecatedVerificationToken_revoke_at_idx", "revoke_at"),
        Index("LiteLLM_DeprecatedVerificationToken_token_key", "token", unique=True),
    )

class LiteLLMDeletedVerificationToken(Base):
    __tablename__ = "LiteLLM_DeletedVerificationToken"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    token: Mapped[str]
    key_name: Mapped[Optional[str]]
    key_alias: Mapped[Optional[str]]
    soft_budget_cooldown: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    total_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    expires: Mapped[Optional[datetime]]
    models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    aliases: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    config: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    user_id: Mapped[Optional[str]]
    team_id: Mapped[Optional[str]]
    agent_id: Mapped[Optional[str]]
    project_id: Mapped[Optional[str]]
    permissions: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    max_parallel_requests: Mapped[Optional[int]]
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSONB, default=lambda: {}, server_default=text("'{}'"))
    blocked: Mapped[Optional[bool]]
    tpm_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    rpm_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    tpd_limit: Mapped[Optional[int]] = mapped_column(BigInteger)
    max_budget: Mapped[Optional[float]]
    budget_duration: Mapped[Optional[str]]
    budget_reset_at: Mapped[Optional[datetime]]
    allowed_cache_controls: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    allowed_routes: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    key_type: Mapped[Optional[str]]
    policies: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    access_group_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    model_spend: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    model_max_budget: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    budget_fallbacks: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    router_settings: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    budget_id: Mapped[Optional[str]]
    organization_id: Mapped[Optional[str]]
    object_permission_id: Mapped[Optional[str]]
    created_at: Mapped[Optional[datetime]]
    created_by: Mapped[Optional[str]]
    updated_at: Mapped[Optional[datetime]]
    updated_by: Mapped[Optional[str]]
    settings_updated_at: Mapped[Optional[datetime]]
    last_active: Mapped[Optional[datetime]]
    rotation_count: Mapped[Optional[int]] = mapped_column(default=0, server_default=text("0"))
    auto_rotate: Mapped[Optional[bool]] = mapped_column(default=False, server_default=text("false"))
    rotation_interval: Mapped[Optional[str]]
    last_rotation_at: Mapped[Optional[datetime]]
    key_rotation_at: Mapped[Optional[datetime]]
    deleted_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    deleted_by: Mapped[Optional[str]]
    deleted_by_api_key: Mapped[Optional[str]]
    litellm_changed_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_DeletedVerificationToken_token_idx", "token"),
        Index("LiteLLM_DeletedVerificationToken_deleted_at_idx", "deleted_at"),
        Index("LiteLLM_DeletedVerificationToken_user_id_idx", "user_id"),
        Index("LiteLLM_DeletedVerificationToken_team_id_idx", "team_id"),
        Index("LiteLLM_DeletedVerificationToken_organization_id_idx", "organization_id"),
        Index("LiteLLM_DeletedVerificationToken_key_alias_idx", "key_alias"),
        Index("LiteLLM_DeletedVerificationToken_created_at_idx", "created_at"),
    )

class LiteLLMEndUserTable(Base):
    __tablename__ = "LiteLLM_EndUserTable"

    user_id: Mapped[str] = mapped_column(primary_key=True)
    alias: Mapped[Optional[str]]
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    allowed_model_region: Mapped[Optional[str]]
    default_model: Mapped[Optional[str]]
    models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    budget_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_BudgetTable.budget_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_EndUserTable_budget_id_fkey"))
    object_permission_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_ObjectPermissionTable.object_permission_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_EndUserTable_object_permission_id_fkey"))
    blocked: Mapped[bool] = mapped_column(default=False, server_default=text("false"))

    litellm_budget_table: Mapped[Optional["LiteLLMBudgetTable"]] = relationship(back_populates="end_users")
    object_permission: Mapped[Optional["LiteLLMObjectPermissionTable"]] = relationship(back_populates="end_users")

class LiteLLMModelAccessGroupBudgetTable(Base):
    __tablename__ = "LiteLLM_ModelAccessGroupBudgetTable"

    access_group_name: Mapped[str] = mapped_column(primary_key=True)
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    budget_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_BudgetTable.budget_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_ModelAccessGroupBudgetTable_budget_id_fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[Optional[str]]

    litellm_budget_table: Mapped[Optional["LiteLLMBudgetTable"]] = relationship(back_populates="model_access_groups")

class LiteLLMTagTable(Base):
    __tablename__ = "LiteLLM_TagTable"

    tag_name: Mapped[str] = mapped_column(primary_key=True)
    description: Mapped[Optional[str]]
    models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    model_info: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    budget_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_BudgetTable.budget_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_TagTable_budget_id_fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    litellm_budget_table: Mapped[Optional["LiteLLMBudgetTable"]] = relationship(back_populates="tags")

class LiteLLMConfig(Base):
    __tablename__ = "LiteLLM_Config"

    param_name: Mapped[str] = mapped_column(primary_key=True)
    param_value: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    last_run_at: Mapped[Optional[datetime]]
    reload_revision: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))

class LiteLLMSpendLogs(Base):
    __tablename__ = "LiteLLM_SpendLogs"

    request_id: Mapped[str] = mapped_column(primary_key=True)
    call_type: Mapped[str]
    api_key: Mapped[str] = mapped_column(default="", server_default=text("''"))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    total_tokens: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    prompt_tokens: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    completion_tokens: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    start_time: Mapped[datetime] = mapped_column("startTime")
    end_time: Mapped[datetime] = mapped_column("endTime")
    request_duration_ms: Mapped[Optional[int]]
    completion_start_time: Mapped[Optional[datetime]] = mapped_column("completionStartTime")
    model: Mapped[str] = mapped_column(default="", server_default=text("''"))
    model_id: Mapped[Optional[str]] = mapped_column(default="", server_default=text("''"))
    model_group: Mapped[Optional[str]] = mapped_column(default="", server_default=text("''"))
    custom_llm_provider: Mapped[Optional[str]] = mapped_column(default="", server_default=text("''"))
    api_base: Mapped[Optional[str]] = mapped_column(default="", server_default=text("''"))
    user: Mapped[Optional[str]] = mapped_column(default="", server_default=text("''"))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    cache_hit: Mapped[Optional[str]] = mapped_column(default="", server_default=text("''"))
    cache_key: Mapped[Optional[str]] = mapped_column(default="", server_default=text("''"))
    request_tags: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: [], server_default=text("'[]'"))
    team_id: Mapped[Optional[str]]
    organization_id: Mapped[Optional[str]]
    end_user: Mapped[Optional[str]]
    requester_ip_address: Mapped[Optional[str]]
    messages: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    response: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    session_id: Mapped[Optional[str]]
    status: Mapped[Optional[str]]
    mcp_namespaced_tool_name: Mapped[Optional[str]]
    billing_agent_id: Mapped[Optional[str]]
    agent_id: Mapped[Optional[str]]
    proxy_server_request: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    litellm_call_id: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_SpendLogs_startTime_idx", "startTime"),
        Index("LiteLLM_SpendLogs_startTime_request_id_idx", "startTime", "request_id"),
        Index("LiteLLM_SpendLogs_end_user_idx", "end_user"),
        Index("LiteLLM_SpendLogs_session_id_idx", "session_id"),
        Index("LiteLLM_SpendLogs_litellm_call_id_idx", "litellm_call_id"),
        Index("LiteLLM_SpendLogs_api_key_startTime_idx", "api_key", "startTime"),
    )

class LiteLLMBudgetWindowSpend(Base):
    __tablename__ = "LiteLLM_BudgetWindowSpend"

    entity_type: Mapped[str] = mapped_column(primary_key=True)
    entity_id: Mapped[str] = mapped_column(primary_key=True)
    window_duration: Mapped[str] = mapped_column(primary_key=True)
    window_start: Mapped[datetime]
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

class LiteLLMErrorLogs(Base):
    __tablename__ = "LiteLLM_ErrorLogs"

    request_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    start_time: Mapped[datetime] = mapped_column("startTime")
    end_time: Mapped[datetime] = mapped_column("endTime")
    api_base: Mapped[str] = mapped_column(default="", server_default=text("''"))
    model_group: Mapped[str] = mapped_column(default="", server_default=text("''"))
    litellm_model_name: Mapped[str] = mapped_column(default="", server_default=text("''"))
    model_id: Mapped[str] = mapped_column(default="", server_default=text("''"))
    request_kwargs: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    exception_type: Mapped[str] = mapped_column(default="", server_default=text("''"))
    exception_string: Mapped[str] = mapped_column(default="", server_default=text("''"))
    status_code: Mapped[str] = mapped_column(default="", server_default=text("''"))

class LiteLLMUserNotifications(Base):
    __tablename__ = "LiteLLM_UserNotifications"

    request_id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str]
    models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    justification: Mapped[str]
    status: Mapped[str]

class LiteLLMTeamMembership(Base):
    __tablename__ = "LiteLLM_TeamMembership"

    user_id: Mapped[str] = mapped_column(primary_key=True)
    team_id: Mapped[str] = mapped_column(primary_key=True)
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    total_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    budget_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_BudgetTable.budget_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_TeamMembership_budget_id_fkey"))

    litellm_budget_table: Mapped[Optional["LiteLLMBudgetTable"]] = relationship(back_populates="team_membership")

class LiteLLMOrganizationMembership(Base):
    __tablename__ = "LiteLLM_OrganizationMembership"

    user_id: Mapped[str] = mapped_column(ForeignKey("LiteLLM_UserTable.user_id", ondelete="RESTRICT", onupdate="CASCADE", name="LiteLLM_OrganizationMembership_user_id_fkey"), primary_key=True)
    organization_id: Mapped[str] = mapped_column(ForeignKey("LiteLLM_OrganizationTable.organization_id", ondelete="RESTRICT", onupdate="CASCADE", name="LiteLLM_OrganizationMembership_organization_id_fkey"), primary_key=True)
    user_role: Mapped[Optional[str]]
    spend: Mapped[Optional[float]] = mapped_column(default=0, server_default=text("0"))
    budget_id: Mapped[Optional[str]] = mapped_column(ForeignKey("LiteLLM_BudgetTable.budget_id", ondelete="SET NULL", onupdate="CASCADE", name="LiteLLM_OrganizationMembership_budget_id_fkey"))
    created_at: Mapped[Optional[datetime]] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_OrganizationMembership_user_id_organization_id_key", "user_id", "organization_id", unique=True),
    )

    user: Mapped["LiteLLMUserTable"] = relationship(back_populates="organization_memberships")
    organization: Mapped["LiteLLMOrganizationTable"] = relationship(back_populates="members")
    litellm_budget_table: Mapped[Optional["LiteLLMBudgetTable"]] = relationship(back_populates="organization_membership")

class LiteLLMInvitationLink(Base):
    __tablename__ = "LiteLLM_InvitationLink"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    user_id: Mapped[str] = mapped_column(ForeignKey("LiteLLM_UserTable.user_id", ondelete="RESTRICT", onupdate="CASCADE", name="LiteLLM_InvitationLink_user_id_fkey"))
    is_accepted: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    accepted_at: Mapped[Optional[datetime]]
    expires_at: Mapped[datetime]
    created_at: Mapped[datetime]
    created_by: Mapped[str] = mapped_column(ForeignKey("LiteLLM_UserTable.user_id", ondelete="RESTRICT", onupdate="CASCADE", name="LiteLLM_InvitationLink_created_by_fkey"))
    updated_at: Mapped[datetime]
    updated_by: Mapped[str] = mapped_column(ForeignKey("LiteLLM_UserTable.user_id", ondelete="RESTRICT", onupdate="CASCADE", name="LiteLLM_InvitationLink_updated_by_fkey"))

    lite_llm_user_table_user: Mapped["LiteLLMUserTable"] = relationship(foreign_keys=[user_id], back_populates="invitations_user")
    lite_llm_user_table_created: Mapped["LiteLLMUserTable"] = relationship(foreign_keys=[created_by], back_populates="invitations_created")
    lite_llm_user_table_updated: Mapped["LiteLLMUserTable"] = relationship(foreign_keys=[updated_by], back_populates="invitations_updated")

class LiteLLMAuditLog(Base):
    __tablename__ = "LiteLLM_AuditLog"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    changed_by: Mapped[str] = mapped_column(default="", server_default=text("''"))
    changed_by_api_key: Mapped[str] = mapped_column(default="", server_default=text("''"))
    action: Mapped[str]
    table_name: Mapped[str]
    object_id: Mapped[str]
    before_value: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    updated_values: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

class LiteLLMDailyUserSpend(Base):
    __tablename__ = "LiteLLM_DailyUserSpend"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    user_id: Mapped[Optional[str]]
    date_: Mapped[str] = mapped_column("date")
    api_key: Mapped[str]
    model: Mapped[Optional[str]]
    model_group: Mapped[Optional[str]]
    custom_llm_provider: Mapped[Optional[str]]
    mcp_namespaced_tool_name: Mapped[Optional[str]]
    endpoint: Mapped[Optional[str]]
    prompt_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    completion_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_read_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_creation_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_saved_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    prompt_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    gateway_injected_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    autorouter_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    guardrail_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    api_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    successful_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    failed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    total_response_time_ms: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    timed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_DailyUserSpend_date_idx", "date"),
        Index("LiteLLM_DailyUserSpend_user_id_date_idx", "user_id", "date"),
        Index("LiteLLM_DailyUserSpend_api_key_idx", "api_key"),
        Index("LiteLLM_DailyUserSpend_api_key_date_idx", "api_key", "date"),
        Index("LiteLLM_DailyUserSpend_model_idx", "model"),
        Index("LiteLLM_DailyUserSpend_mcp_namespaced_tool_name_idx", "mcp_namespaced_tool_name"),
        Index("LiteLLM_DailyUserSpend_endpoint_idx", "endpoint"),
        Index("LiteLLM_DailyUserSpend_user_id_date_api_key_model_custom_ll_key", "user_id", "date", "api_key", "model", "custom_llm_provider", "mcp_namespaced_tool_name", "endpoint", unique=True),
    )

class LiteLLMDailyGlobalSpend(Base):
    __tablename__ = "LiteLLM_DailyGlobalSpend"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    date_: Mapped[str] = mapped_column("date")
    model: Mapped[Optional[str]]
    model_group: Mapped[Optional[str]]
    custom_llm_provider: Mapped[Optional[str]]
    mcp_namespaced_tool_name: Mapped[Optional[str]]
    endpoint: Mapped[Optional[str]]
    prompt_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    completion_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_read_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_creation_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_saved_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    prompt_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    gateway_injected_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    autorouter_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    guardrail_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    api_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    successful_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    failed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    total_response_time_ms: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    timed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_DailyGlobalSpend_date_idx", "date"),
        Index("LiteLLM_DailyGlobalSpend_date_model_model_group_custom_llm__key", "date", "model", "model_group", "custom_llm_provider", "mcp_namespaced_tool_name", "endpoint", unique=True),
    )

class LiteLLMDailyOrganizationSpend(Base):
    __tablename__ = "LiteLLM_DailyOrganizationSpend"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    organization_id: Mapped[Optional[str]]
    date_: Mapped[str] = mapped_column("date")
    api_key: Mapped[str]
    model: Mapped[Optional[str]]
    model_group: Mapped[Optional[str]]
    custom_llm_provider: Mapped[Optional[str]]
    mcp_namespaced_tool_name: Mapped[Optional[str]]
    endpoint: Mapped[Optional[str]]
    prompt_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    completion_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_read_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_creation_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_saved_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    prompt_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    gateway_injected_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    autorouter_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    guardrail_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    api_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    successful_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    failed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    total_response_time_ms: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    timed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_DailyOrganizationSpend_date_idx", "date"),
        Index("LiteLLM_DailyOrganizationSpend_organization_id_date_idx", "organization_id", "date"),
        Index("LiteLLM_DailyOrganizationSpend_api_key_idx", "api_key"),
        Index("LiteLLM_DailyOrganizationSpend_model_idx", "model"),
        Index("LiteLLM_DailyOrganizationSpend_mcp_namespaced_tool_name_idx", "mcp_namespaced_tool_name"),
        Index("LiteLLM_DailyOrganizationSpend_endpoint_idx", "endpoint"),
        Index("LiteLLM_DailyOrganizationSpend_organization_id_date_api_key_key", "organization_id", "date", "api_key", "model", "custom_llm_provider", "mcp_namespaced_tool_name", "endpoint", unique=True),
    )

class LiteLLMDailyCustomerMemberSpend(Base):
    __tablename__ = "LiteLLM_DailyCustomerMemberSpend"

    id: Mapped[str] = mapped_column(primary_key=True)
    date_: Mapped[str] = mapped_column("date")
    customer_org_id: Mapped[str]
    customer_team_id: Mapped[str] = mapped_column(default="", server_default=text("''"))
    customer_member_id: Mapped[str] = mapped_column(default="", server_default=text("''"))
    model: Mapped[str] = mapped_column(default="", server_default=text("''"))
    model_group: Mapped[str] = mapped_column(default="", server_default=text("''"))
    custom_llm_provider: Mapped[str] = mapped_column(default="", server_default=text("''"))
    mcp_namespaced_tool_name: Mapped[str] = mapped_column(default="", server_default=text("''"))
    endpoint: Mapped[str] = mapped_column(default="", server_default=text("''"))
    api_key: Mapped[str] = mapped_column(default="", server_default=text("''"))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    prompt_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    completion_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_read_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_creation_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_saved_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    prompt_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    gateway_injected_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    autorouter_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    guardrail_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    api_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    successful_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    failed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    total_response_time_ms: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    timed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_DailyCustomerMemberSpend_date_idx", "date"),
        Index("LiteLLM_DailyCustomerMemberSpend_customer_org_id_date_idx", "customer_org_id", "date"),
        Index("LiteLLM_DailyCustomerMemberSpend_customer_team_id_date_idx", "customer_team_id", "date"),
        Index("LiteLLM_DailyCustomerMemberSpend_customer_member_id_date_idx", "customer_member_id", "date"),
        Index("LiteLLM_DailyCustomerMemberSpend_api_key_idx", "api_key"),
        Index("LiteLLM_DailyCustomerMemberSpend_model_idx", "model"),
    )

class LiteLLMDailyEndUserSpend(Base):
    __tablename__ = "LiteLLM_DailyEndUserSpend"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    end_user_id: Mapped[Optional[str]]
    date_: Mapped[str] = mapped_column("date")
    api_key: Mapped[str]
    model: Mapped[Optional[str]]
    model_group: Mapped[Optional[str]]
    custom_llm_provider: Mapped[Optional[str]]
    mcp_namespaced_tool_name: Mapped[Optional[str]]
    endpoint: Mapped[Optional[str]]
    prompt_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    completion_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_read_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_creation_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_saved_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    prompt_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    gateway_injected_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    autorouter_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    guardrail_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    api_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    successful_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    failed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    total_response_time_ms: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    timed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_DailyEndUserSpend_date_idx", "date"),
        Index("LiteLLM_DailyEndUserSpend_end_user_id_date_idx", "end_user_id", "date"),
        Index("LiteLLM_DailyEndUserSpend_api_key_idx", "api_key"),
        Index("LiteLLM_DailyEndUserSpend_model_idx", "model"),
        Index("LiteLLM_DailyEndUserSpend_mcp_namespaced_tool_name_idx", "mcp_namespaced_tool_name"),
        Index("LiteLLM_DailyEndUserSpend_endpoint_idx", "endpoint"),
        Index("LiteLLM_DailyEndUserSpend_end_user_id_date_api_key_model_cu_key", "end_user_id", "date", "api_key", "model", "custom_llm_provider", "mcp_namespaced_tool_name", "endpoint", unique=True),
    )

class LiteLLMDailyAgentSpend(Base):
    __tablename__ = "LiteLLM_DailyAgentSpend"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    agent_id: Mapped[Optional[str]]
    date_: Mapped[str] = mapped_column("date")
    api_key: Mapped[str]
    model: Mapped[Optional[str]]
    model_group: Mapped[Optional[str]]
    custom_llm_provider: Mapped[Optional[str]]
    mcp_namespaced_tool_name: Mapped[Optional[str]]
    endpoint: Mapped[Optional[str]]
    prompt_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    completion_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_read_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_creation_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_saved_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    prompt_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    gateway_injected_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    autorouter_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    guardrail_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    api_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    successful_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    failed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    total_response_time_ms: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    timed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_DailyAgentSpend_date_idx", "date"),
        Index("LiteLLM_DailyAgentSpend_agent_id_date_idx", "agent_id", "date"),
        Index("LiteLLM_DailyAgentSpend_api_key_idx", "api_key"),
        Index("LiteLLM_DailyAgentSpend_model_idx", "model"),
        Index("LiteLLM_DailyAgentSpend_mcp_namespaced_tool_name_idx", "mcp_namespaced_tool_name"),
        Index("LiteLLM_DailyAgentSpend_endpoint_idx", "endpoint"),
        Index("LiteLLM_DailyAgentSpend_agent_id_date_api_key_model_custom__key", "agent_id", "date", "api_key", "model", "custom_llm_provider", "mcp_namespaced_tool_name", "endpoint", unique=True),
    )

class LiteLLMDailyTeamSpend(Base):
    __tablename__ = "LiteLLM_DailyTeamSpend"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    team_id: Mapped[Optional[str]]
    date_: Mapped[str] = mapped_column("date")
    api_key: Mapped[str]
    model: Mapped[Optional[str]]
    model_group: Mapped[Optional[str]]
    custom_llm_provider: Mapped[Optional[str]]
    mcp_namespaced_tool_name: Mapped[Optional[str]]
    endpoint: Mapped[Optional[str]]
    prompt_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    completion_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_read_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_creation_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_saved_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    prompt_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    gateway_injected_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    autorouter_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    guardrail_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    api_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    successful_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    failed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    total_response_time_ms: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    timed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    ptu_flat_cost: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_DailyTeamSpend_date_idx", "date"),
        Index("LiteLLM_DailyTeamSpend_team_id_date_idx", "team_id", "date"),
        Index("LiteLLM_DailyTeamSpend_api_key_idx", "api_key"),
        Index("LiteLLM_DailyTeamSpend_model_idx", "model"),
        Index("LiteLLM_DailyTeamSpend_mcp_namespaced_tool_name_idx", "mcp_namespaced_tool_name"),
        Index("LiteLLM_DailyTeamSpend_endpoint_idx", "endpoint"),
        Index("LiteLLM_DailyTeamSpend_team_id_date_api_key_model_custom_ll_key", "team_id", "date", "api_key", "model", "custom_llm_provider", "mcp_namespaced_tool_name", "endpoint", unique=True),
    )

class LiteLLMDailyTagSpend(Base):
    __tablename__ = "LiteLLM_DailyTagSpend"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    request_id: Mapped[Optional[str]]
    tag: Mapped[Optional[str]]
    date_: Mapped[str] = mapped_column("date")
    api_key: Mapped[str]
    model: Mapped[Optional[str]]
    model_group: Mapped[Optional[str]]
    custom_llm_provider: Mapped[Optional[str]]
    mcp_namespaced_tool_name: Mapped[Optional[str]]
    endpoint: Mapped[Optional[str]]
    prompt_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    completion_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_read_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cache_creation_input_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_saved_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    compression_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    prompt_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    gateway_injected_caching_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    autorouter_savings_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    guardrail_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    api_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    successful_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    failed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    total_response_time_ms: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    timed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_DailyTagSpend_date_idx", "date"),
        Index("LiteLLM_DailyTagSpend_tag_date_idx", "tag", "date"),
        Index("LiteLLM_DailyTagSpend_api_key_idx", "api_key"),
        Index("LiteLLM_DailyTagSpend_model_idx", "model"),
        Index("LiteLLM_DailyTagSpend_mcp_namespaced_tool_name_idx", "mcp_namespaced_tool_name"),
        Index("LiteLLM_DailyTagSpend_endpoint_idx", "endpoint"),
        Index("LiteLLM_DailyTagSpend_tag_date_api_key_model_custom_llm_pro_key", "tag", "date", "api_key", "model", "custom_llm_provider", "mcp_namespaced_tool_name", "endpoint", unique=True),
    )

class LiteLLMProxyWorkerHeartbeat(Base):
    __tablename__ = "LiteLLM_ProxyWorkerHeartbeat"

    worker_id: Mapped[str] = mapped_column(primary_key=True)
    hostname: Mapped[str]
    started_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_heartbeat_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class LiteLLMCronJob(Base):
    __tablename__ = "LiteLLM_CronJob"

    cronjob_id: Mapped[str] = mapped_column(primary_key=True)
    pod_id: Mapped[str]
    status: Mapped[str] = mapped_column(Enum("ACTIVE", "INACTIVE", name="JobStatus"), default="INACTIVE", server_default=text("'INACTIVE'"))
    last_updated: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    ttl: Mapped[datetime]

class LiteLLMScheduledJobRun(Base):
    __tablename__ = "LiteLLM_ScheduledJobRun"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    job_name: Mapped[str]
    lock_group: Mapped[str]
    schedule: Mapped[str]
    scheduled_for: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=3))
    status: Mapped[str] = mapped_column(Enum("PENDING", "RUNNING", "SUCCEEDED", "FAILED", "CANCELLED", name="ScheduledJobRunStatus"), default="PENDING", server_default=text("'PENDING'"))
    claimed_by: Mapped[Optional[str]]
    claimed_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=3))
    lease_expires_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=3))
    finished_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=3))
    created_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("LiteLLM_ScheduledJobRun_job_name_status_idx", "job_name", "status"),
        Index("LiteLLM_ScheduledJobRun_lock_group_status_idx", "lock_group", "status"),
    )

class LiteLLMManagedFileTable(Base):
    __tablename__ = "LiteLLM_ManagedFileTable"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    unified_file_id: Mapped[str]
    file_object: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    model_mappings: Mapped[dict[str, Any]] = mapped_column(JSONB)
    flat_model_file_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    storage_backend: Mapped[Optional[str]]
    storage_url: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    team_id: Mapped[Optional[str]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    updated_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_ManagedFileTable_unified_file_id_key", "unified_file_id", unique=True),
        Index("LiteLLM_ManagedFileTable_unified_file_id_idx", "unified_file_id"),
        Index("LiteLLM_ManagedFileTable_flat_model_file_ids_idx", "flat_model_file_ids"),
        Index("LiteLLM_ManagedFileTable_team_id_created_at_idx", "team_id", "created_at"),
        Index("LiteLLM_ManagedFileTable_created_by_created_at_idx", "created_by", "created_at"),
    )

class LiteLLMManagedObjectTable(Base):
    __tablename__ = "LiteLLM_ManagedObjectTable"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    unified_object_id: Mapped[str]
    model_object_id: Mapped[str]
    file_object: Mapped[dict[str, Any]] = mapped_column(JSONB)
    file_purpose: Mapped[str]
    status: Mapped[Optional[str]]
    batch_processed: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    team_id: Mapped[Optional[str]]
    org_id: Mapped[Optional[str]]
    api_key: Mapped[Optional[str]]
    request_tags: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: [], server_default=text("'[]'"))
    end_user_id: Mapped[Optional[str]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    updated_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_ManagedObjectTable_unified_object_id_key", "unified_object_id", unique=True),
        Index("LiteLLM_ManagedObjectTable_model_object_id_key", "model_object_id", unique=True),
        Index("LiteLLM_ManagedObjectTable_unified_object_id_idx", "unified_object_id"),
        Index("LiteLLM_ManagedObjectTable_model_object_id_idx", "model_object_id"),
        Index("LiteLLM_ManagedObjectTable_team_id_created_at_idx", "team_id", "created_at"),
    )

class LiteLLMManagedFileContentTable(Base):
    __tablename__ = "LiteLLM_ManagedFileContentTable"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    content: Mapped[bytes]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class LiteLLMManagedVectorStoreTable(Base):
    __tablename__ = "LiteLLM_ManagedVectorStoreTable"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    unified_resource_id: Mapped[str]
    resource_object: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    model_mappings: Mapped[dict[str, Any]] = mapped_column(JSONB)
    flat_model_resource_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    storage_backend: Mapped[Optional[str]]
    storage_url: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    team_id: Mapped[Optional[str]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    updated_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_ManagedVectorStoreTable_unified_resource_id_key", "unified_resource_id", unique=True),
        Index("LiteLLM_ManagedVectorStoreTable_unified_resource_id_idx", "unified_resource_id"),
        Index("LiteLLM_ManagedVectorStoreTable_team_id_created_at_idx", "team_id", "created_at"),
    )

class LiteLLMManagedVectorStoresTable(Base):
    __tablename__ = "LiteLLM_ManagedVectorStoresTable"

    vector_store_id: Mapped[str] = mapped_column(primary_key=True)
    custom_llm_provider: Mapped[str]
    vector_store_name: Mapped[Optional[str]]
    vector_store_description: Mapped[Optional[str]]
    vector_store_metadata: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    litellm_credential_name: Mapped[Optional[str]]
    litellm_params: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    team_id: Mapped[Optional[str]]
    user_id: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_ManagedVectorStoresTable_team_id_idx", "team_id"),
        Index("LiteLLM_ManagedVectorStoresTable_user_id_idx", "user_id"),
    )

class LiteLLMGuardrailsTable(Base):
    __tablename__ = "LiteLLM_GuardrailsTable"

    guardrail_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    guardrail_name: Mapped[str]
    litellm_params: Mapped[dict[str, Any]] = mapped_column(JSONB)
    guardrail_info: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    team_id: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    status: Mapped[str] = mapped_column(default="active", server_default=text("'active'"))
    submitted_at: Mapped[Optional[datetime]]
    reviewed_at: Mapped[Optional[datetime]]

    __table_args__ = (
        Index("LiteLLM_GuardrailsTable_guardrail_name_key", "guardrail_name", unique=True),
        Index("LiteLLM_GuardrailsTable_status_idx", "status"),
    )

class LiteLLMDailyGuardrailMetrics(Base):
    __tablename__ = "LiteLLM_DailyGuardrailMetrics"

    guardrail_id: Mapped[str] = mapped_column(primary_key=True)
    date_: Mapped[str] = mapped_column("date", primary_key=True)
    requests_evaluated: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    passed_count: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    blocked_count: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    flagged_count: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    avg_score: Mapped[Optional[float]]
    avg_latency_ms: Mapped[Optional[float]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_DailyGuardrailMetrics_date_idx", "date"),
        Index("LiteLLM_DailyGuardrailMetrics_guardrail_id_idx", "guardrail_id"),
    )

class LiteLLMDailyGuardrailUsageUnits(Base):
    __tablename__ = "LiteLLM_DailyGuardrailUsageUnits"

    guardrail_id: Mapped[str] = mapped_column(primary_key=True)
    date_: Mapped[str] = mapped_column("date", primary_key=True)
    team_id: Mapped[str] = mapped_column(primary_key=True)
    api_key: Mapped[str] = mapped_column(primary_key=True)
    usage_unit: Mapped[str] = mapped_column(primary_key=True)
    units: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    cost: Mapped[Optional[float]]
    untracked_units: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_DailyGuardrailUsageUnits_date_idx", "date"),
    )

class LiteLLMDailyPolicyMetrics(Base):
    __tablename__ = "LiteLLM_DailyPolicyMetrics"

    policy_id: Mapped[str] = mapped_column(primary_key=True)
    date_: Mapped[str] = mapped_column("date", primary_key=True)
    requests_evaluated: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    passed_count: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    blocked_count: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    flagged_count: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    avg_score: Mapped[Optional[float]]
    avg_latency_ms: Mapped[Optional[float]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_DailyPolicyMetrics_date_idx", "date"),
        Index("LiteLLM_DailyPolicyMetrics_policy_id_idx", "policy_id"),
    )

class LiteLLMSpendLogGuardrailIndex(Base):
    __tablename__ = "LiteLLM_SpendLogGuardrailIndex"

    request_id: Mapped[str] = mapped_column(primary_key=True)
    guardrail_id: Mapped[str] = mapped_column(primary_key=True)
    policy_id: Mapped[Optional[str]]
    start_time: Mapped[datetime]

    __table_args__ = (
        Index("LiteLLM_SpendLogGuardrailIndex_guardrail_id_start_time_idx", "guardrail_id", "start_time"),
        Index("LiteLLM_SpendLogGuardrailIndex_policy_id_start_time_idx", "policy_id", "start_time"),
    )

class LiteLLMSpendLogToolIndex(Base):
    __tablename__ = "LiteLLM_SpendLogToolIndex"

    request_id: Mapped[str] = mapped_column(primary_key=True)
    tool_name: Mapped[str] = mapped_column(primary_key=True)
    start_time: Mapped[datetime]

    __table_args__ = (
        Index("LiteLLM_SpendLogToolIndex_tool_name_start_time_idx", "tool_name", "start_time"),
        Index("LiteLLM_SpendLogToolIndex_start_time_idx", "start_time"),
    )

class LiteLLMDailyToolSpend(Base):
    __tablename__ = "LiteLLM_DailyToolSpend"

    date_: Mapped[str] = mapped_column("date", primary_key=True)
    tool_name: Mapped[str] = mapped_column(primary_key=True)
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    total_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    request_count: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

class LiteLLMDailyModelUsage(Base):
    __tablename__ = "LiteLLM_DailyModelUsage"

    date_: Mapped[str] = mapped_column("date", primary_key=True)
    model_group: Mapped[str] = mapped_column(primary_key=True)
    model: Mapped[str] = mapped_column(primary_key=True)
    custom_llm_provider: Mapped[str] = mapped_column(primary_key=True)
    task_type: Mapped[str] = mapped_column(primary_key=True)
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    prompt_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    completion_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    request_count: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    successful_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    failed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    total_response_time_ms: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    timed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_DailyModelUsage_date_idx", "date"),
        Index("LiteLLM_DailyModelUsage_model_group_idx", "model_group"),
    )

class LiteLLMDailyGatewayRequests(Base):
    __tablename__ = "LiteLLM_DailyGatewayRequests"

    date_: Mapped[str] = mapped_column("date", primary_key=True)
    category: Mapped[str] = mapped_column(primary_key=True)
    route: Mapped[str] = mapped_column(primary_key=True)
    successful_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    failed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_DailyGatewayRequests_date_idx", "date"),
    )

class LiteLLMDailyGatewayFailedRequests(Base):
    __tablename__ = "LiteLLM_DailyGatewayFailedRequests"

    date_: Mapped[str] = mapped_column("date", primary_key=True)
    category: Mapped[str] = mapped_column(primary_key=True)
    route: Mapped[str] = mapped_column(primary_key=True)
    status_code: Mapped[int] = mapped_column(primary_key=True)
    failed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_DailyGatewayFailedRequests_date_idx", "date"),
    )

class LiteLLMDailyRequestErrors(Base):
    __tablename__ = "LiteLLM_DailyRequestErrors"

    date_: Mapped[str] = mapped_column("date", primary_key=True)
    api_key: Mapped[str] = mapped_column(primary_key=True)
    team_id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column(primary_key=True)
    model_group: Mapped[str] = mapped_column(primary_key=True)
    status_code: Mapped[int] = mapped_column(primary_key=True)
    failed_requests: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_DailyRequestErrors_date_idx", "date"),
    )

class LiteLLMPromptTable(Base):
    __tablename__ = "LiteLLM_PromptTable"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    prompt_id: Mapped[str]
    version: Mapped[int] = mapped_column(default=1, server_default=text("1"))
    environment: Mapped[str] = mapped_column(default="development", server_default=text("'development'"))
    created_by: Mapped[Optional[str]]
    litellm_params: Mapped[dict[str, Any]] = mapped_column(JSONB)
    prompt_info: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_PromptTable_prompt_id_environment_idx", "prompt_id", "environment"),
        Index("LiteLLM_PromptTable_prompt_id_idx", "prompt_id"),
        Index("LiteLLM_PromptTable_prompt_id_version_environment_key", "prompt_id", "version", "environment", unique=True),
    )

class LiteLLMHealthCheckTable(Base):
    __tablename__ = "LiteLLM_HealthCheckTable"

    health_check_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    model_name: Mapped[str]
    model_id: Mapped[Optional[str]]
    status: Mapped[str]
    healthy_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    unhealthy_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    error_message: Mapped[Optional[str]]
    response_time_ms: Mapped[Optional[float]]
    details: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    checked_by: Mapped[Optional[str]]
    checked_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_HealthCheckTable_model_name_idx", "model_name"),
        Index("LiteLLM_HealthCheckTable_checked_at_idx", "checked_at"),
        Index("LiteLLM_HealthCheckTable_status_idx", "status"),
        Index("LiteLLM_HealthCheckTable_model_id_model_name_checked_at_idx", "model_id", "model_name", "checked_at"),
    )

class LiteLLMSearchToolsTable(Base):
    __tablename__ = "LiteLLM_SearchToolsTable"

    search_tool_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    search_tool_name: Mapped[str]
    litellm_params: Mapped[dict[str, Any]] = mapped_column(JSONB)
    search_tool_info: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("LiteLLM_SearchToolsTable_search_tool_name_key", "search_tool_name", unique=True),
    )

class LiteLLMSSOConfig(Base):
    __tablename__ = "LiteLLM_SSOConfig"

    id: Mapped[str] = mapped_column(primary_key=True)
    sso_settings: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

class LiteLLMManagedVectorStoreIndexTable(Base):
    __tablename__ = "LiteLLM_ManagedVectorStoreIndexTable"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    index_name: Mapped[str]
    litellm_params: Mapped[dict[str, Any]] = mapped_column(JSONB)
    index_info: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    updated_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_ManagedVectorStoreIndexTable_index_name_key", "index_name", unique=True),
    )

class LiteLLMCacheConfig(Base):
    __tablename__ = "LiteLLM_CacheConfig"

    id: Mapped[str] = mapped_column(primary_key=True)
    cache_settings: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

class LiteLLMUISettings(Base):
    __tablename__ = "LiteLLM_UISettings"

    id: Mapped[str] = mapped_column(primary_key=True)
    ui_settings: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

class LiteLLMConfigOverrides(Base):
    __tablename__ = "LiteLLM_ConfigOverrides"

    config_type: Mapped[str] = mapped_column(primary_key=True)
    config_value: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

class LiteLLMSkillsTable(Base):
    __tablename__ = "LiteLLM_SkillsTable"

    skill_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    display_title: Mapped[Optional[str]]
    description: Mapped[Optional[str]]
    instructions: Mapped[Optional[str]]
    source: Mapped[str] = mapped_column(default="custom", server_default=text("'custom'"))
    latest_version: Mapped[Optional[str]]
    file_content: Mapped[Optional[bytes]]
    file_name: Mapped[Optional[str]]
    file_type: Mapped[Optional[str]]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[Optional[str]]

class LiteLLMPolicyTable(Base):
    __tablename__ = "LiteLLM_PolicyTable"

    policy_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    policy_name: Mapped[str]
    version_number: Mapped[int] = mapped_column(default=1, server_default=text("1"))
    version_status: Mapped[str] = mapped_column(default="production", server_default=text("'production'"))
    parent_version_id: Mapped[Optional[str]]
    is_latest: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    published_at: Mapped[Optional[datetime]]
    production_at: Mapped[Optional[datetime]]
    inherit: Mapped[Optional[str]]
    description: Mapped[Optional[str]]
    guardrails_add: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    guardrails_remove: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    condition: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    pipeline: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_PolicyTable_policy_name_version_status_idx", "policy_name", "version_status"),
        Index("LiteLLM_PolicyTable_policy_name_version_number_key", "policy_name", "version_number", unique=True),
    )

class LiteLLMPolicyAttachmentTable(Base):
    __tablename__ = "LiteLLM_PolicyAttachmentTable"

    attachment_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    policy_name: Mapped[str]
    scope: Mapped[Optional[str]]
    teams: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    keys: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    tags: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    priority: Mapped[Optional[int]]
    is_default: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[Optional[str]]

class LiteLLMToolTable(Base):
    __tablename__ = "LiteLLM_ToolTable"

    tool_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    tool_name: Mapped[str]
    origin: Mapped[Optional[str]]
    input_policy: Mapped[str] = mapped_column(default="untrusted", server_default=text("'untrusted'"))
    output_policy: Mapped[str] = mapped_column(default="untrusted", server_default=text("'untrusted'"))
    call_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    assignments: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    key_hash: Mapped[Optional[str]]
    team_id: Mapped[Optional[str]]
    key_alias: Mapped[Optional[str]]
    user_agent: Mapped[Optional[str]]
    last_used_at: Mapped[Optional[datetime]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_ToolTable_tool_name_key", "tool_name", unique=True),
        Index("LiteLLM_ToolTable_input_policy_idx", "input_policy"),
        Index("LiteLLM_ToolTable_output_policy_idx", "output_policy"),
        Index("LiteLLM_ToolTable_team_id_idx", "team_id"),
    )

class LiteLLMAccessGroupTable(Base):
    __tablename__ = "LiteLLM_AccessGroupTable"

    access_group_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    access_group_name: Mapped[str]
    description: Mapped[Optional[str]]
    access_model_names: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    access_mcp_server_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    mcp_tool_permissions: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    access_agent_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    access_skill_names: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    access_vector_store_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    assigned_team_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    assigned_key_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_AccessGroupTable_access_group_name_key", "access_group_name", unique=True),
    )

class LiteLLMClaudeCodePluginTable(Base):
    __tablename__ = "LiteLLM_ClaudeCodePluginTable"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    version: Mapped[Optional[str]]
    description: Mapped[Optional[str]]
    manifest_json: Mapped[Optional[str]]
    files_json: Mapped[Optional[str]] = mapped_column(default="{}", server_default=text("'{}'"))
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    created_at: Mapped[Optional[datetime]] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by: Mapped[Optional[str]]
    anonymous_download_count: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))

    __table_args__ = (
        Index("LiteLLM_ClaudeCodePluginTable_name_key", "name", unique=True),
    )

    versions: Mapped[list["LiteLLMClaudeCodePluginVersion"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="plugin")
    downloads: Mapped[list["LiteLLMClaudeCodePluginDownload"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="plugin")
    uploaded_archive: Mapped[Optional["LiteLLMClaudeCodePluginUpload"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="plugin")

class LiteLLMClaudeCodePluginVersion(Base):
    __tablename__ = "LiteLLM_ClaudeCodePluginVersion"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    plugin_id: Mapped[str] = mapped_column(ForeignKey("LiteLLM_ClaudeCodePluginTable.id", ondelete="CASCADE", onupdate="CASCADE", name="LiteLLM_ClaudeCodePluginVersion_plugin_id_fkey"))
    version: Mapped[Optional[str]]
    description: Mapped[Optional[str]]
    manifest_json: Mapped[Optional[str]]
    published_at: Mapped[Optional[datetime]]
    published_by: Mapped[Optional[str]]
    archive_available: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    content: Mapped[Optional[bytes]]

    __table_args__ = (
        Index("LiteLLM_ClaudeCodePluginVersion_plugin_id_published_at_idx", "plugin_id", "published_at"),
        Index("LiteLLM_ClaudeCodePluginVersion_plugin_id_version_key", "plugin_id", "version", unique=True),
    )

    plugin: Mapped["LiteLLMClaudeCodePluginTable"] = relationship(back_populates="versions")

class LiteLLMSkillSubmission(Base):
    __tablename__ = "LiteLLM_SkillSubmission"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    plugin_name: Mapped[str]
    operation: Mapped[str]
    status: Mapped[str] = mapped_column(default="pending_review", server_default=text("'pending_review'"))
    proposal_json: Mapped[str]
    archive_content: Mapped[Optional[bytes]]
    submitted_by: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    base_plugin_id: Mapped[Optional[str]]
    base_updated_at: Mapped[Optional[datetime]]
    reviewed_by: Mapped[Optional[str]]
    reviewed_at: Mapped[Optional[datetime]]
    rejection_reason: Mapped[Optional[str]]
    scan_json: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_SkillSubmission_submitted_by_created_at_id_idx", "submitted_by", "created_at", "id"),
        Index("LiteLLM_SkillSubmission_submitted_by_status_created_at_id_idx", "submitted_by", "status", "created_at", "id"),
        Index("LiteLLM_SkillSubmission_status_created_at_id_idx", "status", "created_at", "id"),
        Index("LiteLLM_SkillSubmission_created_at_id_idx", "created_at", "id"),
    )

class LiteLLMClaudeCodePluginDownload(Base):
    __tablename__ = "LiteLLM_ClaudeCodePluginDownload"

    id: Mapped[int] = mapped_column(BigInteger, primary_key=True, autoincrement=True)
    plugin_id: Mapped[str] = mapped_column(ForeignKey("LiteLLM_ClaudeCodePluginTable.id", ondelete="CASCADE", onupdate="CASCADE", name="LiteLLM_ClaudeCodePluginDownload_plugin_id_fkey"))
    downloaded_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    version: Mapped[Optional[str]]
    digest: Mapped[str]
    authenticated: Mapped[bool]
    user_id: Mapped[Optional[str]]
    user_email: Mapped[Optional[str]]
    key_alias: Mapped[Optional[str]]
    team_id: Mapped[Optional[str]]
    team_alias: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_ClaudeCodePluginDownload_plugin_id_id_idx", "plugin_id", "id"),
    )

    plugin: Mapped["LiteLLMClaudeCodePluginTable"] = relationship(back_populates="downloads")

class LiteLLMClaudeCodePluginUpload(Base):
    __tablename__ = "LiteLLM_ClaudeCodePluginUpload"

    plugin_name: Mapped[str] = mapped_column(ForeignKey("LiteLLM_ClaudeCodePluginTable.name", ondelete="CASCADE", onupdate="CASCADE", name="LiteLLM_ClaudeCodePluginUpload_plugin_name_fkey"), primary_key=True)
    content: Mapped[bytes]

    plugin: Mapped["LiteLLMClaudeCodePluginTable"] = relationship(back_populates="uploaded_archive")

class LiteLLMMemoryTable(Base):
    __tablename__ = "LiteLLM_MemoryTable"

    memory_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    key: Mapped[str]
    value: Mapped[str]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    user_id: Mapped[Optional[str]]
    team_id: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_MemoryTable_key_key", "key", unique=True),
        Index("LiteLLM_MemoryTable_user_id_idx", "user_id"),
        Index("LiteLLM_MemoryTable_team_id_idx", "team_id"),
    )

class LiteLLMAdaptiveRouterState(Base):
    __tablename__ = "LiteLLM_AdaptiveRouterState"

    router_name: Mapped[str] = mapped_column(primary_key=True)
    request_type: Mapped[str] = mapped_column(primary_key=True)
    model_name: Mapped[str] = mapped_column(primary_key=True)
    alpha: Mapped[float]
    beta: Mapped[float]
    total_samples: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    last_updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

class LiteLLMAdaptiveRouterSession(Base):
    __tablename__ = "LiteLLM_AdaptiveRouterSession"

    session_id: Mapped[str] = mapped_column(primary_key=True)
    router_name: Mapped[str] = mapped_column(primary_key=True)
    model_name: Mapped[str] = mapped_column(primary_key=True)
    classified_type: Mapped[str]
    misalignment_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    stagnation_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    disengagement_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    satisfaction_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    failure_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    loop_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    exhaustion_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    last_user_content: Mapped[Optional[str]]
    last_assistant_content: Mapped[Optional[str]]
    tool_call_history: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: [], server_default=text("'[]'"))
    pending_tool_calls: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    turn_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    last_processed_turn: Mapped[int] = mapped_column(default=-1, server_default=text("-1"))
    clean_credit_awarded: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    terminal_status: Mapped[Optional[int]]
    last_activity_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("idx_adaptive_router_session_activity", "last_activity_at"),
    )

class LiteLLMAutoRouterBaselineComparison(Base):
    __tablename__ = "LiteLLM_AutoRouterBaselineComparison"

    scope: Mapped[str] = mapped_column(primary_key=True)
    api_key: Mapped[str]
    session_id: Mapped[str]
    router_name: Mapped[str]
    initial_equivalent: Mapped[bool]
    revision: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    published_revision: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    history: Mapped[Optional[str]]
    attempted_at: Mapped[Optional[datetime]]
    retired: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_autorouter_baseline_scope", "api_key", "session_id", "router_name"),
        Index("idx_autorouter_baseline_updated", "updated_at"),
    )

class LiteLLMAutoRouterBaselineObservation(Base):
    __tablename__ = "LiteLLM_AutoRouterBaselineObservation"

    request_id: Mapped[str] = mapped_column(primary_key=True)
    scope: Mapped[str]
    started_at: Mapped[float]
    revision: Mapped[int] = mapped_column(BigInteger)
    data: Mapped[str]
    publication: Mapped[Optional[str]]
    conflicted: Mapped[bool] = mapped_column(default=False, server_default=text("false"))

    __table_args__ = (
        Index("idx_autorouter_baseline_event_order", "scope", "started_at", "request_id"),
        Index("idx_autorouter_baseline_event_revision", "scope", "revision", "started_at"),
    )

class LiteLLMAutoRouterSession(Base):
    __tablename__ = "LiteLLM_AutoRouterSession"

    api_key: Mapped[str] = mapped_column(primary_key=True)
    session_id: Mapped[str] = mapped_column(primary_key=True)
    router_name: Mapped[str] = mapped_column(primary_key=True)
    router_type: Mapped[str]
    first_turn_at: Mapped[datetime]
    last_turn_at: Mapped[datetime]
    last_model: Mapped[str]
    models: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    unordered_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    covered_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    cache_hits: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    same_model_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    same_model_hits: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    first_visit_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    first_visit_hits: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    return_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    return_hits: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    return_expired_misses: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    return_within_ttl_misses: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    ttl_5m_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    ttl_1h_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    total_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    saved_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    savings_estimated_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    savings_estimated_actual_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    savings_estimated_saved_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    savings_estimated_baseline_models: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    classifier_cost: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    classifier_cost_recorded_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    tier_turns: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    baseline_models: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))

    __table_args__ = (
        Index("idx_autorouter_session_last_turn", "last_turn_at"),
    )

class LiteLLMAutoRouterUserSession(Base):
    __tablename__ = "LiteLLM_AutoRouterUserSession"

    user_id: Mapped[str] = mapped_column(primary_key=True)
    api_key: Mapped[str] = mapped_column(primary_key=True)
    session_id: Mapped[str] = mapped_column(primary_key=True)
    router_name: Mapped[str] = mapped_column(primary_key=True)
    router_type: Mapped[str]
    first_turn_at: Mapped[datetime]
    last_turn_at: Mapped[datetime]
    last_model: Mapped[str]
    models: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    unordered_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    covered_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    cache_hits: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    same_model_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    same_model_hits: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    first_visit_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    first_visit_hits: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    return_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    return_hits: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    return_expired_misses: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    return_within_ttl_misses: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    ttl_5m_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    ttl_1h_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    total_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    saved_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    savings_estimated_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    savings_estimated_actual_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    savings_estimated_saved_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    savings_estimated_baseline_models: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    classifier_cost: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    classifier_cost_recorded_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    tier_turns: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    baseline_models: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))

    __table_args__ = (
        Index("idx_autorouter_user_session_last_turn", "last_turn_at"),
        Index("idx_autorouter_user_session_user_last_turn", "user_id", "last_turn_at"),
    )

class LiteLLMAutoRouterDailySpend(Base):
    __tablename__ = "LiteLLM_AutoRouterDailySpend"

    date_: Mapped[str] = mapped_column("date", primary_key=True)
    api_key: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column(primary_key=True)
    router_name: Mapped[str] = mapped_column(primary_key=True)
    router_type: Mapped[str] = mapped_column(primary_key=True)
    turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    total_tokens: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    token_recorded_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    saved_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    savings_estimated_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    savings_estimated_actual_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    savings_estimated_saved_spend: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    classifier_cost: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    classifier_cost_recorded_turns: Mapped[int] = mapped_column(default=0, server_default=text("0"))

class LiteLLMShadowEvalJob(Base):
    __tablename__ = "LiteLLM_ShadowEvalJob"

    id: Mapped[str] = mapped_column(primary_key=True)
    group_id: Mapped[str]
    target_type: Mapped[str] = mapped_column(default="key", server_default=text("'key'"))
    target_id: Mapped[str]
    router_name: Mapped[str]
    router_names: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    direction: Mapped[str] = mapped_column(default="forward", server_default=text("'forward'"))
    baseline_model: Mapped[Optional[str]]
    judge_model: Mapped[str]
    shadow_percentage: Mapped[float]
    max_turns: Mapped[int]
    max_budget: Mapped[Optional[float]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[Optional[str]]
    ends_at: Mapped[datetime]
    stopped_at: Mapped[Optional[datetime]]
    stopped_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("LiteLLM_ShadowEvalJob_group_id_idx", "group_id"),
        Index("LiteLLM_ShadowEvalJob_target_type_target_id_idx", "target_type", "target_id"),
        Index("LiteLLM_ShadowEvalJob_created_at_idx", "created_at"),
    )

class LiteLLMShadowEvalAttempt(Base):
    __tablename__ = "LiteLLM_ShadowEvalAttempt"

    id: Mapped[str] = mapped_column(primary_key=True)
    job_id: Mapped[str]
    request_id: Mapped[str]
    outcome: Mapped[str]
    router_name: Mapped[Optional[str]]
    tier: Mapped[Optional[str]]
    real_model: Mapped[Optional[str]]
    shadow_model: Mapped[Optional[str]]
    confidence: Mapped[Optional[float]]
    judge_cost: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    shadow_cost: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    real_cost: Mapped[Optional[float]]
    real_classifier_cost: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    shadow_classifier_cost: Mapped[float] = mapped_column(default=0, server_default=text("0"))
    real_cache_hit: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    error: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("LiteLLM_ShadowEvalAttempt_job_id_idx", "job_id"),
    )

class LiteLLMShadowEvalFunnel(Base):
    __tablename__ = "LiteLLM_ShadowEvalFunnel"

    job_id: Mapped[str] = mapped_column(primary_key=True)
    not_sampled: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    unjudgeable: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    shed: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    withheld: Mapped[int] = mapped_column(default=0, server_default=text("0"))

class LiteLLMWorkflowRun(Base):
    __tablename__ = "LiteLLM_WorkflowRun"

    run_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    session_id: Mapped[str] = mapped_column(default=lambda: str(uuid_mod.uuid4()))
    workflow_type: Mapped[str]
    status: Mapped[str] = mapped_column(default="pending", server_default=text("'pending'"))
    created_by: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)
    input: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    output: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))

    __table_args__ = (
        Index("LiteLLM_WorkflowRun_session_id_key", "session_id", unique=True),
        Index("LiteLLM_WorkflowRun_workflow_type_status_idx", "workflow_type", "status"),
        Index("LiteLLM_WorkflowRun_session_id_idx", "session_id"),
        Index("LiteLLM_WorkflowRun_created_at_idx", "created_at"),
        Index("LiteLLM_WorkflowRun_created_by_idx", "created_by"),
    )

    events: Mapped[list["LiteLLMWorkflowEvent"]] = relationship(passive_deletes="all", back_populates="run")
    messages: Mapped[list["LiteLLMWorkflowMessage"]] = relationship(passive_deletes="all", back_populates="run")

class LiteLLMWorkflowEvent(Base):
    __tablename__ = "LiteLLM_WorkflowEvent"

    event_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    run_id: Mapped[str] = mapped_column(ForeignKey("LiteLLM_WorkflowRun.run_id", ondelete="RESTRICT", onupdate="CASCADE", name="LiteLLM_WorkflowEvent_run_id_fkey"))
    event_type: Mapped[str]
    step_name: Mapped[str]
    sequence_number: Mapped[int]
    data: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("LiteLLM_WorkflowEvent_run_id_idx", "run_id"),
        Index("LiteLLM_WorkflowEvent_run_id_sequence_number_key", "run_id", "sequence_number", unique=True),
    )

    run: Mapped["LiteLLMWorkflowRun"] = relationship(back_populates="events")

class LiteLLMWorkflowMessage(Base):
    __tablename__ = "LiteLLM_WorkflowMessage"

    message_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    run_id: Mapped[str] = mapped_column(ForeignKey("LiteLLM_WorkflowRun.run_id", ondelete="RESTRICT", onupdate="CASCADE", name="LiteLLM_WorkflowMessage_run_id_fkey"))
    role: Mapped[str]
    content: Mapped[str]
    sequence_number: Mapped[int]
    session_id: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("LiteLLM_WorkflowMessage_run_id_idx", "run_id"),
        Index("LiteLLM_WorkflowMessage_run_id_sequence_number_key", "run_id", "sequence_number", unique=True),
    )

    run: Mapped["LiteLLMWorkflowRun"] = relationship(back_populates="messages")

class LiteLLMBackgroundInteractionSettlement(Base):
    __tablename__ = "LiteLLM_BackgroundInteractionSettlement"

    interaction_id: Mapped[str] = mapped_column(primary_key=True)
    custom_llm_provider: Mapped[str]
    create_context: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    claimed_at: Mapped[Optional[datetime]]
    claimed_by: Mapped[Optional[str]]
    settled_at: Mapped[Optional[datetime]]
    outcome: Mapped[Optional[str]]

    __table_args__ = (
        Index("idx_background_interaction_settlement_claimed_at", "claimed_at"),
        Index("idx_background_interaction_settlement_settled_at", "settled_at"),
    )

class LiteLLMBackgroundResponseSettlement(Base):
    __tablename__ = "LiteLLM_BackgroundResponseSettlement"

    response_id: Mapped[str] = mapped_column(primary_key=True)
    settled_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class LiteLLMLens(Base):
    __tablename__ = "LiteLLM_Lens"

    id: Mapped[str] = mapped_column(primary_key=True)
    version: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)
    due_at: Mapped[Optional[datetime]] = mapped_column(server_default=text("'1970-01-01 00:00:00'::timestamp without time zone"))

    __table_args__ = (
        Index("LiteLLM_Lens_due_at_idx", "due_at"),
    )

    reviews: Mapped[list["LiteLLMLensReview"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="lens")

class LiteLLMLensRun(Base):
    __tablename__ = "LiteLLM_LensRun"

    id: Mapped[str] = mapped_column(primary_key=True)
    lens_id: Mapped[str]
    created_at: Mapped[datetime]
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)

    __table_args__ = (
        Index("LiteLLM_LensRun_lens_id_created_at_idx", "lens_id", "created_at"),
    )

class LiteLLMLensReview(Base):
    __tablename__ = "LiteLLM_LensReview"

    lens_id: Mapped[str] = mapped_column(ForeignKey("LiteLLM_Lens.id", ondelete="CASCADE", onupdate="CASCADE", name="LiteLLM_LensReview_lens_id_fkey"), primary_key=True)
    criteria_key: Mapped[str] = mapped_column(primary_key=True)
    execution_id: Mapped[str] = mapped_column(primary_key=True)
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)

    lens: Mapped["LiteLLMLens"] = relationship(back_populates="reviews")

class LiteLLMLensWorker(Base):
    __tablename__ = "LiteLLM_LensWorker"

    id: Mapped[str] = mapped_column(primary_key=True)
    token_hash: Mapped[str]
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)

    __table_args__ = (
        Index("LiteLLM_LensWorker_token_hash_key", "token_hash", unique=True),
    )

class LiteLLMLensIngestionKey(Base):
    __tablename__ = "LiteLLM_LensIngestionKey"

    id: Mapped[str] = mapped_column(primary_key=True)
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)

class LiteLLMLensDataset(Base):
    __tablename__ = "LiteLLM_LensDataset"

    id: Mapped[str] = mapped_column(primary_key=True)
    revision: Mapped[int] = mapped_column(primary_key=True)
    created_at: Mapped[datetime]
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)

class LiteLLMLensSignalConfig(Base):
    __tablename__ = "LiteLLM_LensSignalConfig"

    id: Mapped[str] = mapped_column(primary_key=True)
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)

class LiteLLMLensTraceSignal(Base):
    __tablename__ = "LiteLLM_LensTraceSignal"

    trace_id: Mapped[str] = mapped_column(primary_key=True)
    trace_ref: Mapped[str] = mapped_column(primary_key=True)
    config_key: Mapped[str]
    span_count: Mapped[int]
    claimed_until: Mapped[Optional[datetime]]
    classified_at: Mapped[Optional[datetime]]
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)

class LiteLLMTelemetryReport(Base):
    __tablename__ = "LiteLLM_TelemetryReport"

    id: Mapped[str] = mapped_column(primary_key=True)
    window_start: Mapped[float]
    window_end: Mapped[float]
    report: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("LiteLLM_TelemetryReport_window_end_idx", "window_end"),
        Index("LiteLLM_TelemetryReport_created_at_idx", "created_at"),
    )

class LiteLLMSpendLogDelivery(Base):
    __tablename__ = "LiteLLM_SpendLogDelivery"

    delivery_id: Mapped[str] = mapped_column(primary_key=True, server_default=text("(gen_random_uuid())::text"))
    request_id: Mapped[str]
    state: Mapped[str] = mapped_column(Enum("pending", "applied", "parked", name="AccountingDeliveryState"), default="pending", server_default=text("'pending'"))
    accounting_payload: Mapped[dict[str, Any]] = mapped_column(JSONB)
    is_batch: Mapped[bool]
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    next_attempt_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    applied_at: Mapped[Optional[datetime]]
    last_error: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("LiteLLM_SpendLogDelivery_state_next_attempt_at_idx", "state", "next_attempt_at"),
        Index("LiteLLM_SpendLogDelivery_state_applied_at_idx", "state", "applied_at"),
        Index("LiteLLM_SpendLogDelivery_request_id_idx", "request_id"),
    )

class LiteLLMSubscriptionAccountTable(Base):
    __tablename__ = "LiteLLM_SubscriptionAccountTable"

    subscription_account_id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    custom_llm_provider: Mapped[str]
    account_id: Mapped[str]
    label: Mapped[Optional[str]]
    monthly_fee: Mapped[float]
    currency: Mapped[str]
    billing_period_start: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[str]

    __table_args__ = (
        Index("LiteLLM_SubscriptionAccountTable_provider_account_key", "custom_llm_provider", "account_id", unique=True),
    )

class LiteLLMAlert(Base):
    __tablename__ = "LiteLLM_Alert"

    id: Mapped[int] = mapped_column(BigInteger, primary_key=True, autoincrement=True)
    detection_key: Mapped[str] = mapped_column(String(64))
    type: Mapped[str] = mapped_column(String(32))
    resource_type: Mapped[str] = mapped_column(String(16))
    resource_id: Mapped[str] = mapped_column(String(1024))
    status: Mapped[str] = mapped_column(String(16), default="open", server_default=text("'open'"))
    window_start: Mapped[datetime]
    window_end: Mapped[datetime]
    evidence: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[Optional[str]] = mapped_column(String(1024))

    __table_args__ = (
        Index("LiteLLM_Alert_detection_key_key", "detection_key", unique=True),
        Index("LiteLLM_Alert_status_id_idx", "status", "id"),
    )

class LiteLLMPerformanceAlertState(Base):
    __tablename__ = "LiteLLM_PerformanceAlertState"

    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    alerts_used: Mapped[Optional[int]]
    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=False)
    owner: Mapped[str] = mapped_column(String(32))
    next_run_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=3))
    day: Mapped[Optional[str]] = mapped_column(String(10))
    scope: Mapped[Optional[str]] = mapped_column(String(16))
    resume_after: Mapped[str] = mapped_column(String(1024), default="", server_default=text("''"))
    wrapped: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    checked: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    skipped: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    start_scope: Mapped[Optional[str]] = mapped_column(String(16))
    start_after: Mapped[str] = mapped_column(String(1024), default="", server_default=text("''"))
    completed_at: Mapped[Optional[datetime]]
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
